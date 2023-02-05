using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WarcosTools
{

	public class MaterialInstancer : Editor
	{
		[MenuItem("Warco's Tools/Material Instancer %#i")]
		public static void Instance()
		{
			if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<MeshRenderer>() == null)
			{
				Debug.LogError("No valid object selected");
				return;
			}
			Material mat = Selection.activeGameObject.GetComponent<MeshRenderer>().sharedMaterial;
			Selection.activeGameObject.GetComponent<MeshRenderer>().sharedMaterial = new Material(mat);
		}
	}

}