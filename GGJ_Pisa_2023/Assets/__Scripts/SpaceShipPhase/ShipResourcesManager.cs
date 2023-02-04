using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ResourceType
{
	Food,
	Water,
	Oxygen
}

public class ShipResourcesManager : MonoBehaviour
{

	[BoxGroup("Resources Amount")] [HorizontalGroup("Resources Amount/Levels")] [LabelText("Food")] [LabelWidth(40)] [SerializeField]
	[ReadOnly]private float foodLevel;
	[ReadOnly][HorizontalGroup("Resources Amount/Levels")] [LabelText("Water")] [LabelWidth(40)] [SerializeField]
	private float waterLevel;
	[ReadOnly][HorizontalGroup("Resources Amount/Levels")] [LabelText("Oxygen")] [LabelWidth(50)] [SerializeField]
	private float oxygenLevel;

	public static Action oxygenFinished;

	private void Update()
	{
		foodLevel = PlayerResources.currentFood;
		waterLevel = PlayerResources.currentWater;
		oxygenLevel = PlayerResources.currentOxygen;
	}

	private void OnEnable()
	{
		ShipInteractionPoint.onExitPoint += StopConsume;
		TravelManager.vojageStarted += ConsumeOxygen;
	}

	private void OnDisable()
	{
		ShipInteractionPoint.onExitPoint -= StopConsume;
	}

	private void StopConsume()
	{
		StopAllCoroutines();
	}

	public void ConsumeOxygen(float consume)
	{
		StartCoroutine(ConsumeCoroutine());

		IEnumerator ConsumeCoroutine()
		{
			PlayerResources.currentOxygen -= consume;
			if (oxygenLevel < 0)
			{
				oxygenFinished?.Invoke();
			}
			yield return null;
		}

		StartCoroutine(ConsumeCoroutine());
	}

public void StartConsume(ResourceType resourceType, float consume)
{
	if (resourceType == ResourceType.Oxygen)
	{
		ConsumeOxygen(consume);
	}
	StartCoroutine(ConsumeCoroutine());

	IEnumerator ConsumeCoroutine()
	{
		switch (resourceType)
		{
			case ResourceType.Food:
				PlayerResources.currentFood -= consume;
				if (foodLevel < 0)
				{
					foodLevel = 0;
				}
				break;
			case ResourceType.Water:
				PlayerResources.currentWater -= consume;
				if (waterLevel < 0)
				{
					waterLevel = 0;
				}
				break;
		}
		yield return null;
		StartCoroutine(ConsumeCoroutine());
	}
}
}