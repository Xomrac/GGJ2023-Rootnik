using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using Riutilizzabile;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ResourceType
{
	Food,
	Water,
	Oxygen
}

public class ShipResourcesManager : Singleton<ShipResourcesManager>
{

	[BoxGroup("Resources Amount")] [HorizontalGroup("Resources Amount/Levels")] [LabelText("Food")] [LabelWidth(40)] [SerializeField]
	[ReadOnly]private float foodLevel;
	[ReadOnly][HorizontalGroup("Resources Amount/Levels")] [LabelText("Water")] [LabelWidth(40)] [SerializeField]
	private float waterLevel;
	[ReadOnly][HorizontalGroup("Resources Amount/Levels")] [LabelText("Oxygen")] [LabelWidth(50)] [SerializeField]
	private float oxygenLevel;

	public static Action oxygenFinished;

	public List<ShipInteractionPoint> plants;

	public ShipInteractionPoint currentInteractionPoint                         ;

	private void Update()
	{
		foodLevel = PlayerResources.currentFood;
		waterLevel = PlayerResources.currentWater;
		oxygenLevel = PlayerResources.currentOxygen;
		var counter = 0;
		foreach (ShipInteractionPoint plant in plants)
		{
			if (plant.isDead)
			{
				counter++;
			}
		}
		if (counter>=Mathf.Ceil(plants.Count/2))
		{
			//DEADDDD
		}
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

	[Button]
	private void SetResources(Vector3 resourcesValue)
	{
		PlayerResources.currentFood = resourcesValue.x;
		PlayerResources.currentWater = resourcesValue.y;
		PlayerResources.currentOxygen = resourcesValue.z;
	}

	public void StopConsume()
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
	Debug.Log($"Starting using {resourceType}");
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
				else
				{
					currentInteractionPoint.needs[ResourceType.Food] -= consume;
				}
				break;
			case ResourceType.Water:
				PlayerResources.currentWater -= consume;
				if (waterLevel < 0)
				{
					waterLevel = 0;
				}
				else
				{
					currentInteractionPoint.needs[ResourceType.Food] -= consume;
				}
				break;
		}
		yield return null;
		StartCoroutine(ConsumeCoroutine());
	}
}
}