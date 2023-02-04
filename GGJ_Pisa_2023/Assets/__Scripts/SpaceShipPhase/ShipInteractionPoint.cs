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
		public static event Action<ResourceType, float> onEnteringPoint;

		public static event Action onExitPoint;

		[LabelWidth(100)]public Dictionary<ResourceType, float> consumes = new Dictionary<ResourceType, float>();
		private float timeToStartConsume;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				StartCoroutine(Waiter());
			}

			IEnumerator Waiter()
			{
				yield return new WaitForSeconds(timeToStartConsume);
				foreach (KeyValuePair<ResourceType, float> consume in consumes)
				{
					onEnteringPoint?.Invoke(consume.Key, consume.Value);
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				StopAllCoroutines();
				onExitPoint?.Invoke();
			}
		}

	}

}