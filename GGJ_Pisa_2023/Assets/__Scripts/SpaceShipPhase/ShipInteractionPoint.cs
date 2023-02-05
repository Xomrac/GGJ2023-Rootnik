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

		public bool isDead;

		public static event Action onExitPoint;

		[LabelWidth(100)] public Dictionary<ResourceType, float> consumes = new Dictionary<ResourceType, float>();
		[LabelWidth(100)] public Dictionary<ResourceType, float> needs = new Dictionary<ResourceType, float>();

		public Vector2 maxValues;
		public Vector2 requests;

		public float baseWhineMultiplier;

		public void StartWhining(float f)
		{
			StartWhining();
		}
		public void StartWhining()
		{
			StartCoroutine(Whine());

			IEnumerator Whine()
			{
				yield return null;
				for (int i = 0; i < needs.Keys.Count; i++)
				{
					needs[needs.Keys.ToList()[i]] += requests.x * baseWhineMultiplier * (PlayerResources.deepestRootPlanetDistance / 100);
					if (needs[needs.Keys.ToList()[i]] > maxValues[i])
					{
						isDead = true;
						StopAllCoroutines();
						onExitPoint?.Invoke();
						entered = false;
						gameObject.SetActive(false);
					}
				}
				StartCoroutine(Whine());
			}
		}

		private void OnEnable()
		{
			TravelManager.vojageStarted += StartWhining;
			TravelManager.vojageEnded += StopWhining;
		}

		private void OnDisable()
		{
			TravelManager.vojageStarted -= StartWhining;
			TravelManager.vojageEnded -= StopWhining;
		}

		public void StopWhining()
		{
			StopAllCoroutines();
		}

		[ReadOnly] public bool entered = false;

		private void OnTriggerEnter(Collider other)
		{
			if (entered) return;
			if (other.CompareTag("Player"))
			{
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