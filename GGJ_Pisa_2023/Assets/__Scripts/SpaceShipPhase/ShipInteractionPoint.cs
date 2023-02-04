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
	public class ShipInteractionPoint : MonoBehaviour
	{
		public static event Action<ResourceType> onEnteringPoint;
		
		public static event Action onExitPoint;

		[SerializeField] [EnumToggleButtons] private List<ResourceType> resourceToConsume;
		[SerializeField] private float timeToStartConsume;

#if UNITY_EDITOR

		private void OnValidate()
		{
			resourceToConsume = resourceToConsume.Distinct().ToList();
		}

#endif

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				StartCoroutine(Waiter());
			}
			IEnumerator Waiter()
			{
				yield return new WaitForSeconds(timeToStartConsume);
				foreach (ResourceType resourceType in resourceToConsume)
				{
					onEnteringPoint?.Invoke(resourceType);
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