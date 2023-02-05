using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Riutilizzabile;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlanetsConnector : Singleton<PlanetsConnector>
{
   [SerializeField] private PlanetSetupper levelSelectionManager;

   [SerializeField] public List<PlanetStats> roamingPlanets;
   
   



   public void CheckIfRooted(int index)
   {
      levelSelectionManager.planets[index].rootFeedback.SetActive(roamingPlanets[index].wasVisited);
   }

   [Button]
   public void FetchRoamingPlanets()
   {
      roamingPlanets = FindObjectsOfType<PlanetStats>().ToList();
   }
}
