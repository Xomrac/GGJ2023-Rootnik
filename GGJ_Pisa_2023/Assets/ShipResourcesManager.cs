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
    
    [BoxGroup("Resources Amount")]
    [HorizontalGroup("Resources Amount/Levels")][LabelText("Food")][LabelWidth(40)][SerializeField] private float foodLevel;
    public float FoodLevel => foodLevel;
    [HorizontalGroup("Resources Amount/Levels")][LabelText("Water")][LabelWidth(40)][SerializeField] private float waterLevel;
    public float WaterLevel => waterLevel;
    [HorizontalGroup("Resources Amount/Levels")][LabelText("Oxygen")][LabelWidth(50)][SerializeField] private float oxygenLevel;
    public float OxygenLevel => oxygenLevel;
    
    [BoxGroup("Resources Consume Per Frame")]
    [HorizontalGroup("Resources Consume Per Frame/Depletes")][LabelWidth(40)][LabelText("Food")][SerializeField] private float foodDepletionRate;
    public float FoodDepletionRate => foodDepletionRate;
    [HorizontalGroup("Resources Consume Per Frame/Depletes")][LabelWidth(40)][LabelText("Water")][SerializeField] private float waterDepletionRate;
    public float WaterDepletionRate => waterDepletionRate;
    [HorizontalGroup("Resources Consume Per Frame/Depletes")][LabelWidth(50)][LabelText("Oxygen")][SerializeField] private float oxygenDepletionRate;
    public float OxygenDepletionRate => oxygenDepletionRate;

    private void OnEnable()
    {
        ShipInteractionPoint.onEnteringPoint += StartConsume;
        ShipInteractionPoint.onExitPoint += StopConsume;
    }

    private void OnDisable()
    {
        ShipInteractionPoint.onEnteringPoint -= StartConsume;
        ShipInteractionPoint.onExitPoint -= StopConsume;
    }

    private void StopConsume()
    {
        StopAllCoroutines();
    }
    public void StartConsume(ResourceType resourceType)
    {
        StartCoroutine(ConsumeCoroutine());
        IEnumerator ConsumeCoroutine()
        {
            switch (resourceType)
            {
                case ResourceType.Food:
                    foodLevel -= foodDepletionRate;
                    if (foodLevel<0)
                    {
                        foodLevel = 0;
                    }
                    break;
                case ResourceType.Water:
                    waterLevel -= waterDepletionRate;
                    if (waterLevel<0)
                    {
                        waterLevel = 0;
                    }
                    break;
                case ResourceType.Oxygen:
                    oxygenLevel -= oxygenDepletionRate;
                    if (oxygenLevel<0)
                    {
                        oxygenLevel = 0;
                    }
                    break;
            }
            yield return null;
            StartCoroutine(ConsumeCoroutine());
        }
        
    }
    
}
