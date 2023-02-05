using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetSetupper : SerializedMonoBehaviour
{
    public List<SelectablePlanet> planets;
    public List<Sprite> planetsSprite;
    public List<string> planetsNames;

    public Dictionary<Fertility, Sprite> fertilitySprites;


    [Button] 
    public void FetchPlanets()
    {
        planets = new List<SelectablePlanet>();
        planets = FindObjectsOfType<SelectablePlanet>().ToList();
        planets.OrderBy(o=>o.name);
    }

    [Button]
    public void SetupPlanets()
    {
        foreach (SelectablePlanet planet in planets)
        {
            planet.displayName = planetsNames[Random.Range(0, planetsNames.Count - 1)];
            planet.misterySprite.sprite = planetsSprite[Random.Range(0, planetsSprite.Count - 1)];
            planet.spriteRenderer.sprite = fertilitySprites[planet.planetFertility];
            planet.rootFeedback.SetActive(false);
            planet.index = planets.IndexOf(planet);
            planet.distance = planet.index;
        }
    }


    public string CreateRandomCoords()
    {
        string coords = "Coords";
        return coords;
    }
}
