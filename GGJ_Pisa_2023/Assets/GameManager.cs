using System.Collections;
using System.Collections.Generic;
using Jam;
using Riutilizzabile;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] public TopdownController spaceShipCharacter;
    [SerializeField] public MovementAroundPlanet planetCharacter;



    public void SwitchToPlanetRoaming()
    {
        spaceShipCharacter.gameObject.SetActive(false);
        planetCharacter.gameObject.SetActive(true);
    }

    public void SwitchToSpaceShip()
    {
        spaceShipCharacter.gameObject.SetActive(true);
        planetCharacter.gameObject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
