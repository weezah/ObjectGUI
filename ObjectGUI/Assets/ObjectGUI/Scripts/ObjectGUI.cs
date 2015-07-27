// Author: ts_matt@hotmail.com
using UnityEngine;

namespace ObjectGUI
{
	/// <summary>
	/// Allows to drag a gameobject and select the monobehaviour to be drawn in the editor
	/// </summary>
	public class ObjectGUI : MonoBehaviour
	{
		public GameObject targetObject;

		[HideInInspector]
		public MonoBehaviour
			targetBehaviour;
	}
}