// Author: ts_matt@hotmail.com
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace ObjectGUI
{
	/// <summary>
	/// Defines the area for the GUI and the placement of the hidden toggle button
	/// Collects all the Monobehaviours defined in the ObjectGUI on this GameObject and draws them
	/// 
	/// </summary>
	public class ObjectGUIManager : SingletonBehaviour<ObjectGUIManager>
	{
		/// <summary>
		/// Array of objects to be drawn collected on this gameobject
		/// </summary>
		private ObjectGUI[] objects;

		/// <summary>
		/// The user can specify hi and low res skins according to the resolution of the device
		/// Skins are optional and can be left empty
		/// </summary>
		public GUISkin LowResSkin, HiResSkin;

		/// <summary>
		/// Rectangle area of the GUI
		/// </summary>
		public Rect rectArea = new Rect (0, 0, Screen.width, Screen.height);

		/// <summary>
		/// Rectangle area of the hidden toggle button
		/// </summary>
		public Rect hiddenButtonArea = new Rect (0, Screen.height * .9f, Screen.width * .1f, Screen.height * .1f);

		/// <summary>
		/// Used for indenting while drawing
		/// </summary>
		private int indent = 0;
		private bool lowRes = true;

		/// <summary>
		/// Dictionary that stores the propertydrawers for all supported types
		/// </summary>
		private Dictionary<System.Type, IPropertyDrawer> propertyDrawers = new Dictionary<System.Type, IPropertyDrawer> (){
			{ typeof(int), new IntPropertyDrawer() },
			{ typeof(float), new FloatPropertyDrawer() },
			{ typeof(bool), new BoolPropertyDrawer() },
			{ typeof(Vector2), new Vector2PropertyDrawer() },
			{ typeof(Vector3), new Vector3PropertyDrawer() },
			{ typeof(Vector4), new Vector4PropertyDrawer() },
			{ typeof(Color), new ColorPropertyDrawer() },
		};

		/// <summary>
		/// Shows the GUI if true
		/// </summary>
		public bool showGUI = false;
		
		public Rect Area {
			get { return rectArea; }
			set { rectArea = value; }
		}

		#region Monobehaviour Methods

		void Start ()
		{
			objects = GetComponents<ObjectGUI> ();
		}

		void OnGUI ()
		{
			GUI.skin.button.normal.background = null;
			if (GUI.Button (hiddenButtonArea, ""))
				showGUI = !showGUI;
				
			if (showGUI)
				DrawObjects ();
		}

		#endregion

		#region Drawing methods

		/// <summary>
		/// Draws all the objects
		/// </summary>
		void DrawObjects ()
		{
			// set the skin
			if (Screen.width <= 1024)
				GUI.skin = LowResSkin;
			else
				GUI.skin = HiResSkin;
				
			GUILayout.BeginArea (rectArea);
				
			foreach (var obj in objects) {
				indent = 0;
				DrawObject (obj.targetBehaviour);
			}
				
			GUILayout.EndArea ();
		}
			
		void DrawObject (MonoBehaviour obj)
		{
			GUILayout.BeginVertical ();
			GUILayout.Space (indent);
				
			GUI.skin.label.fontStyle = FontStyle.Bold;
			GUILayout.Label (string.Format ("{0} - {1}", obj.name, obj.GetType ().ToString ()));
			GUI.skin.label.fontStyle = FontStyle.Normal;
				
			FieldInfo [] fields = obj.GetType ().GetFields (BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			foreach (FieldInfo fi in fields) {
				GUILayout.BeginHorizontal ();
				GUILayout.Space (indent);
					

				if (propertyDrawers.ContainsKey (fi.FieldType))
					propertyDrawers [fi.FieldType].Draw (obj, fi);

				GUILayout.EndHorizontal ();
			} // foreach field

			GUILayout.EndVertical ();				
		}

		#endregion
	}

	public interface  IPropertyDrawer
	{
		void Draw (object obj, FieldInfo fi);

	}

	public class Vector2PropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			Vector2 retV = (Vector2)fi.GetValue (obj);
			
			GUILayout.Label (fi.Name);
			string retX = GUILayout.TextField (retV.x.ToString ());
			string retY = GUILayout.TextField (retV.y.ToString ());
			
			if (float.TryParse (retX, out retV.x) && float.TryParse (retY, out retV.y))
				fi.SetValue (obj, retV);
		}
	}

	public class Vector3PropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			Vector3 retV = (Vector3)fi.GetValue (obj);
			
			GUILayout.Label (fi.Name);
			string retX = GUILayout.TextField (retV.x.ToString ());
			string retY = GUILayout.TextField (retV.y.ToString ());
			string retZ = GUILayout.TextField (retV.z.ToString ());
			
			if (float.TryParse (retX, out retV.x) && float.TryParse (retY, out retV.y) && float.TryParse (retZ, out retV.z))
				fi.SetValue (obj, retV);
		}
	}

	public class Vector4PropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			Vector4 retV = (Vector4)fi.GetValue (obj);
			
			GUILayout.Label (fi.Name);
			string retX = GUILayout.TextField (retV.x.ToString ());
			string retY = GUILayout.TextField (retV.y.ToString ());
			string retZ = GUILayout.TextField (retV.z.ToString ());
			string retW = GUILayout.TextField (retV.w.ToString ());
			
			if (float.TryParse (retX, out retV.x) && float.TryParse (retY, out retV.y) && float.TryParse (retZ, out retV.z) && float.TryParse (retW, out retV.w))
				fi.SetValue (obj, retV);
		}
	}

	public class ColorPropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			Color retV = (Color)fi.GetValue (obj);
			
			GUILayout.Label (fi.Name);
			string retR = GUILayout.TextField (retV.r.ToString ());
			string retG = GUILayout.TextField (retV.g.ToString ());
			string retB = GUILayout.TextField (retV.b.ToString ());
			string retA = GUILayout.TextField (retV.a.ToString ());
			
			if (float.TryParse (retR, out retV.r) && float.TryParse (retG, out retV.g) && float.TryParse (retB, out retV.b) && float.TryParse (retA, out retV.a))
				fi.SetValue (obj, retV);
		}
	}

	public class StringPropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			GUILayout.Label (fi.Name);
			string ret = GUILayout.TextField (fi.GetValue (obj).ToString ());
			fi.SetValue (obj, ret);
		}
	}

	public class BoolPropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			bool ret = GUILayout.Toggle ((bool)fi.GetValue (obj), fi.Name);
			fi.SetValue (obj, ret);
		}
	}

	public class IntPropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			GUILayout.Label (fi.Name);
			GUILayout.Label (fi.GetValue (obj).ToString ());

			int intVal;

			var ranges = fi.GetCustomAttributes (typeof(RangeAttribute), false);
			if (ranges.Length > 0) {
				RangeAttribute r = ranges [0] as RangeAttribute;
				intVal = (int)GUILayout.HorizontalSlider ((int)fi.GetValue (obj), (int)r.min, (int)r.max);
			} else {
				int.TryParse (GUILayout.TextField (fi.GetValue (obj).ToString ()), out intVal);
			}

			fi.SetValue (obj, intVal);
		}
	}

	public class FloatPropertyDrawer : IPropertyDrawer
	{
		public void Draw (object obj, FieldInfo fi)
		{
			GUILayout.Label (fi.Name);
			GUILayout.Label (fi.GetValue (obj).ToString ());
			
			float fVal;
			
			var ranges = fi.GetCustomAttributes (typeof(RangeAttribute), false);
			if (ranges.Length > 0) {
				RangeAttribute r = ranges [0] as RangeAttribute;
				fVal = GUILayout.HorizontalSlider ((float)fi.GetValue (obj), r.min, r.max);
				fi.SetValue (obj, fVal);
			} else {
				if (float.TryParse (GUILayout.TextField (fi.GetValue (obj).ToString ()), out fVal))
					fi.SetValue (obj, fVal);
			}
			
	
		}
	}

}
