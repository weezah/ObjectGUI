// Author: ts_matt@hotmail.com

using UnityEngine;
using System.Collections;

/// <summary>
/// This example shows all the types that can be edited
/// </summary>
public class ExampleBehaviour : MonoBehaviour
{
	public int intField;
	public float floatField;
	[Range(0f, 10f)]
	public float
		rangedFloat;
	[Range(0, 10)]
	public int
		rangedIntField;
	public bool boolField;
	public string stringField;
	public Vector2 vector2Field;
	public Vector3 vector3Field;
	public Color colorField;
}