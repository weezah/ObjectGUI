// Author: ts_matt@hotmail.com

using System.Text;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace ObjectGUI
{
	[CustomEditor(typeof(ObjectGUI))]
	public class ObjectGUIEditor : UnityEditor.Editor
	{ 
		private int selectedIdx;

		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();

			ObjectGUI t = (ObjectGUI)target;

			if (t.targetObject) {
				var behaviours = t.targetObject.GetComponents<MonoBehaviour> ();

				if (behaviours.Length > 0) {
					var names = new string[behaviours.Length];
					for (int i = 0; i < behaviours.Length; i++)
						names [i] = behaviours [i].GetType ().Name;

					selectedIdx = EditorGUILayout.Popup (selectedIdx, names);

					t.targetBehaviour = behaviours [selectedIdx];
					return;
				}
			} 

			t.targetBehaviour = null;
		}
	}
}