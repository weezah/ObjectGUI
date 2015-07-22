// Author: ts_matt@hotmail.com
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace ObjectGUI
{
	public class ObjectGUIManager : SingletonBehaviour<ObjectGUIManager>
	{
		private ObjectGUI[] objects;

		public GUISkin LowResSkin, HiResSkin;

		public Rect rectArea = new Rect (0, 0, Screen.width, Screen.height);
		public Rect hiddenButtonArea = new Rect (0, Screen.height * .9f, Screen.width * .1f, Screen.height * .1f);
		private int indent = 0;
		private bool lowRes = true;
		public bool showGUI = false;
		
		public Rect Area {
			get { return rectArea; }
			set { rectArea = value; }
		}

		void Start ()
		{
			objects = GetComponents<ObjectGUI> ();
			lowRes = Screen.width <= 1024;
		}

		void OnGUI ()
		{
			GUI.skin.button.normal.background = null;
			if (GUI.Button (hiddenButtonArea, ""))
				showGUI = !showGUI;
				
			if (showGUI)
				DrawObjects ();
		}
							
		void DrawObjects ()
		{
			GUI.skin = lowRes ? LowResSkin : HiResSkin;
				
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
					
				bool ranged = false;
				RangeAttribute rangeAttr = null;
				var ranges = fi.GetCustomAttributes (typeof(RangeAttribute), false);
				if (ranges.Length > 0) {
					ranged = true;
					rangeAttr = ranges [0] as RangeAttribute;
				}
					
				if (fi.FieldType == typeof(float)) {
						
					if (ranged) {
						DrawFloat (obj, fi, rangeAttr.min, rangeAttr.max);
					} else
						DrawFloat (obj, fi);
						
				} else if (fi.FieldType == typeof(string)) {
					DrawString (obj, fi);
				} else if (fi.FieldType == typeof(int)) {
					DrawInt (obj, fi);
				} else if (fi.FieldType == typeof(bool)) {
					DrawBool (obj, fi);
				} else if (fi.FieldType == typeof(Vector3)) {
					DrawVector3 (obj, fi);
				} else if (fi.FieldType == typeof(Vector2)) {
					DrawVector2 (obj, fi);
				} else if (fi.FieldType == typeof(Color)) {
					DrawColor (obj, fi);
				} else {
					//TODO: draw monobehaviour
					//TODO: draw serializable objects
				}					
				GUILayout.EndHorizontal ();
			}				
			GUILayout.EndVertical ();				
		}
			
		void DrawBool (object obj, FieldInfo fi)
		{
			bool ret = GUILayout.Toggle ((bool)fi.GetValue (obj), fi.Name);
			fi.SetValue (obj, ret);
		}
			
		void DrawString (object obj, FieldInfo fi)
		{
			GUILayout.Label (fi.Name);
			string ret = GUILayout.TextField (fi.GetValue (obj).ToString ());
			fi.SetValue (obj, ret);
		}
			
		void DrawFloat (object obj, FieldInfo fi)
		{
			GUILayout.Label (fi.Name);
			string ret = GUILayout.TextField (fi.GetValue (obj).ToString ());
			float floatVal;
			if (float.TryParse (ret, out floatVal))
				fi.SetValue (obj, floatVal);
		}
			
		void DrawFloat (object obj, FieldInfo fi, float min, float max)
		{
			GUILayout.Label (fi.Name + " " + fi.GetValue (obj).ToString ());
			float ret = GUILayout.HorizontalSlider ((float)fi.GetValue (obj), min, max);
			fi.SetValue (obj, ret);
		}
			
		void DrawInt (object obj, FieldInfo fi)
		{
			GUILayout.Label (fi.Name);
			string ret = GUILayout.TextField (fi.GetValue (obj).ToString ());
			int intVal;
			if (int.TryParse (ret, out intVal))
				fi.SetValue (obj, intVal);
		}
			
		void DrawVector2 (object obj, FieldInfo fi)
		{
			Vector2 retV = (Vector2)fi.GetValue (obj);
				
			GUILayout.Label (fi.Name);
			string retX = GUILayout.TextField (retV.x.ToString ());
			string retY = GUILayout.TextField (retV.y.ToString ());
				
			if (float.TryParse (retX, out retV.x) && float.TryParse (retY, out retV.y))
				fi.SetValue (obj, retV);
		}
			
		void DrawVector3 (object obj, FieldInfo fi)
		{
			Vector3 retV = (Vector3)fi.GetValue (obj);
				
			GUILayout.Label (fi.Name);
			string retX = GUILayout.TextField (retV.x.ToString ());
			string retY = GUILayout.TextField (retV.y.ToString ());
			string retZ = GUILayout.TextField (retV.z.ToString ());
				
			if (float.TryParse (retX, out retV.x) && float.TryParse (retY, out retV.y) && float.TryParse (retZ, out retV.z))
				fi.SetValue (obj, retV);
		}
			
		void DrawColor (object obj, FieldInfo fi)
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

}
