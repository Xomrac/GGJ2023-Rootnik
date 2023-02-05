using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

		public CanvasGroup canvasGroup;

		public float baseWhineMultiplier;

		public float timeOffset;

		[SerializeField] private Image feedbackImage;

		public List<Sprite> feedbackSprites;

		public float warningTime;
		public Vector2 whineThreshold;

		private void Start()
		{
			canvasGroup.alpha = 0;
		}

		public void StartWhining(float f)
		{
			StartCoroutine(wait());
			IEnumerator wait()
			{
				yield return new WaitForSeconds(timeOffset);
			StartWhining();
			}
		}

		private IEnumerator DeathCoroutine()
		{
			var elapsedTime = 0f;
			while (elapsedTime<warningTime)
			{
				for (int i = 0; i < needs.Keys.Count; i++)
				{
					elapsedTime += Time.deltaTime;
					if ((needs[needs.Keys.ToList()[i]] < maxValues[i]))
					{
						StopAllCoroutines();
						StartWhining();
					}
				}
				yield return null;
			}
			isDead = true;
			onExitPoint?.Invoke();
			entered = false;
			gameObject.SetActive(false);
		}
		public void StartWhining()
		{
			StartCoroutine(wait());
			IEnumerator wait()
			{
				yield return new WaitForSeconds(timeOffset);
				StartCoroutine(Whine());
			}
			

			IEnumerator Whine()
			{
				yield return new WaitForSeconds(timeOffset);
				var requestCounter = 0;
				for (int i = 0; i < needs.Keys.Count; i++)
				{
					needs[needs.Keys.ToList()[i]] += requests.x * baseWhineMultiplier * (PlayerResources.deepestRootPlanetDistance / 100);
					if (needs[needs.Keys.ToList()[i]] > maxValues[i])
					{
						StopAllCoroutines();
						
					}
					if (needs[needs.Keys.ToList()[i]] > whineThreshold[i])
					{
						if (needs.Keys.ToList()[i]==ResourceType.Water)
						{
							feedbackImage.sprite = feedbackSprites[0];
						}
						else
						{
							feedbackImage.sprite = feedbackSprites[1];
						}
						requestCounter++;
					}

					switch (requestCounter)
					{
						case 0:
							canvasGroup.alpha = 0;
							break;
						
						case 2:
							canvasGroup.alpha = 1;
							feedbackImage.sprite = feedbackSprites[2];
							break;
						
						default:
							canvasGroup.alpha = 1;
							break;
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