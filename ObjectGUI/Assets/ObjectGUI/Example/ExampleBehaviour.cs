// Author: ts_matt@hotmail.com

using UnityEngine;
using System.Collections;

public class ExampleBehaviour : MonoBehaviour
{

	[Range(0f, 10f)]
	public float
		rangedFloat;
	public int intField;
	public bool boolField;
	public string stringField;
	public Vector2 vector2Field;
	public Vector3 vector3Field;
	public Color colorField;
	public ExampleStruct structField;
	public ExampleBehaviour objectField;

}

[System.Serializable]
public struct ExampleStruct
{
	public float x;
}
