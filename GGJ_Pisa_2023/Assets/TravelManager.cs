using System;
using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using Sirenix.OdinInspector;
using UnityEngine;

public class TravelManager : Singleton<TravelManager>
{
   [ReadOnly] [SerializeField] private float timeToTravel;
   [ReadOnly] [SerializeField] private float currentTravelTime;
   [ReadOnly] [SerializeField] private float oxygenConsume;
   public int planetChosen;
   [ReadOnly] private bool travelling;
   public float RemainingTimePercentage => (currentTravelTime*100)/timeToTravel;

   public static Action<float> timePassed;
   public static Action<float> vojageStarted;
   public static Action vojageEnded;
   public void StartVoyaje(float travelTime)
   {
      vojageStarted?.Invoke(oxygenConsume);
      timeToTravel = travelTime;
      travelling = true;
   }

   private void Update()
   {
      if (!travelling) return;

      currentTravelTime += Time.deltaTime;
      timePassed?.Invoke(currentTravelTime);
      if (currentTravelTime>=timeToTravel)
      {
         travelling = false;
         currentTravelTime = 0;
         vojageEnded?.Invoke();
      }
   }
}
