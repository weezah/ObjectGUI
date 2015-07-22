// Author: ts_matt@hotmail.com

using UnityEngine;

namespace ObjectGUI
{ 
	public class ObjectGUI : MonoBehaviour
	{
		public GameObject targetObject;

		[HideInInspector]
		public MonoBehaviour
			targetBehaviour;
	}
}