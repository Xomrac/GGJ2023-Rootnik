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
    public Vector3 currCenterPlanet;
    private Player player;
    [Range(0, 6.28f)] public float angle;
    public GameObject firstPlanet;

    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
        player.controllers.maps.SetMapsEnabled(false, RewiredConsts.Category.OnPlanet);
        OnLand(firstPlanet);
    }

    private void OnLand(GameObject planet)
    {
        currCenterPlanet = planet.transform.position;
        player.controllers.maps.SetMapsEnabled(true, RewiredConsts.Category.OnPlanet);
        radius= Vector3.Distance(transform.position, planet.transform.position);
        Debug.DrawLine(transform.position, currCenterPlanet, Color.red, 10f);
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
        if (player.GetAxis(RewiredConsts.Action.Move) != 0)
        {
           Move();
        }
    }
    
}