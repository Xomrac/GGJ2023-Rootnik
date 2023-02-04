using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class MovementAroundPlanet : MonoBehaviour
{
    private float radius;
    public float vel;
    private Vector3 currCenterPlanet;
    private Player player;
    [Range(0, 6.28f)] private float angle;
    public PlanetStats firstPlanet;
    public PlanetStats second;

    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
        player.controllers.maps.SetMapsEnabled(false, RewiredConsts.Category.OnPlanet);
        OnLand(firstPlanet);
    }

    private void OnLand(PlanetStats planet)
    {
        transform.position =new Vector3(planet.landingPoint.position.x,planet.landingPoint.position.y+(GetComponent<Collider>().bounds.extents.y*3),planet.landingPoint.position.z);
        currCenterPlanet = planet.transform.position;
        player.controllers.maps.SetMapsEnabled(true, RewiredConsts.Category.OnPlanet);
        radius= Vector3.Distance(transform.position, planet.gameObject.transform.position);
        planet.lastVisitedTime = 0;
        Debug.DrawLine(transform.position, currCenterPlanet, Color.red, 10f);
        foreach (var instancePlanet in PlanetsManager.Instance.planets)
        {
            if (instancePlanet!=planet)
            {
                instancePlanet.lastVisitedTime++;
            }
        }
    }

    public void Move()
    {
        Vector3 newPos;
        angle += vel * player.GetAxis(RewiredConsts.Action.Move) * Time.deltaTime;
        float angledegrees = angle * Mathf.Rad2Deg;
        newPos.x = transform.position.x;
        newPos.y = currCenterPlanet.y + (radius * math.cos(angle));
        newPos.z = currCenterPlanet.z + (radius * math.sin(angle));
        Quaternion rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            -angledegrees);
        transform.position = newPos;
        transform.rotation = rot;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnLand(second);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            OnLand(firstPlanet);
        }
        if (player.GetAxis(RewiredConsts.Action.Move) != 0)
        {
           Move();
        }
    }
    
}