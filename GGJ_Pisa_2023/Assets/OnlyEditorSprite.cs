using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyEditorSprite : MonoBehaviour
{
	private void Awake()
	{
		gameObject.SetActive(false);
	}
}
