using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Jam
{

	[RequireComponent(typeof(BoxCollider))]
	public class ShipInteractionPoint : SerializedMonoBehaviour
	{
		public static event Action<Dictionary<ResourceType, float>> onEnteringPoint;

		public static event Action onExitPoint;

		[LabelWidth(100)]public Dictionary<ResourceType, float> consumes = new Dictionary<ResourceType, float>();
		private float timeToStartConsume;

		[ReadOnly] public bool entered=false;

		private void OnTriggerEnter(Collider other)
		{
			if (entered) return;
			if (other.CompareTag("Player"))
			{
				Debug.Log("Entered!");
				onEnteringPoint?.Invoke(consumes);
				entered = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				StopAllCoroutines();
				onExitPoint?.Invoke();
				entered = false;
			}
		}

	}

}