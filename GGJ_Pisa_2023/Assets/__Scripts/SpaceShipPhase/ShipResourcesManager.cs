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
	private float foodLevel;
	public float FoodLevel => foodLevel;
	[HorizontalGroup("Resources Amount/Levels")] [LabelText("Water")] [LabelWidth(40)] [SerializeField]
	private float waterLevel;
	public float WaterLevel => waterLevel;
	[HorizontalGroup("Resources Amount/Levels")] [LabelText("Oxygen")] [LabelWidth(50)] [SerializeField]
	private float oxygenLevel;
	public float OxygenLevel => oxygenLevel;

	public static Action oxygenFinished;

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
			oxygenLevel -= consume;
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
				foodLevel -= consume;
				if (foodLevel < 0)
				{
					foodLevel = 0;
				}
				break;
			case ResourceType.Water:
				waterLevel -= consume;
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