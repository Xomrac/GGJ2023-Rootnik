using System.Collections.Generic;
using System.Linq;
using Riutilizzabile;
using UnityEngine;

public class PlanetsManager : Singleton<PlanetsManager>
{
   public List<PlanetStats> planets;

   private void Start()
   {
    planets=FindObjectsOfType<PlanetStats>().ToList();
   }
}
