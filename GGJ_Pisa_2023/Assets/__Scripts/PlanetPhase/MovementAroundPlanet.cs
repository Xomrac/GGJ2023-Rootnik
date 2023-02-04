using System;
using System.Collections;
using Rewired;
using Unity.Mathematics;
using UnityEngine;

public class MovementAroundPlanet : MonoBehaviour
{
    private float radius;
    public float vel;
    private Vector3 currCenterPlanet;
    private Player player;
    public Transform piedi;
    [Range(0, 6.28f)] private float angle;
    public PlanetStats firstPlanet;
    public PlanetStats second;
    public PlanetStats curr;
    private Animator animator;
    public GameObject basePrefab;


    private void Start()
    {
        animator = GetComponent<Animator>();
        player = ReInput.players.GetPlayer(0);
        player.controllers.maps.SetMapsEnabled(false, RewiredConsts.Category.OnPlanet);
        OnLand(firstPlanet);
    }

    private void OnLand(PlanetStats planet)
    {
        curr = planet;
        transform.position = new Vector3(planet.landingPoint.position.x,
            planet.landingPoint.position.y+ (GetComponent<Collider>().bounds.extents.y ),
            planet.landingPoint.position.z);
        currCenterPlanet = planet.transform.position;
        player.controllers.maps.SetMapsEnabled(true, RewiredConsts.Category.OnPlanet);
        radius = Vector3.Distance(transform.position, planet.gameObject.transform.position);
        planet.lastVisitedTime = 0;
        Debug.DrawLine(transform.position, currCenterPlanet, Color.red, 10f);
        foreach (var instancePlanet in PlanetsManager.Instance.planets)
        {
            if (instancePlanet != planet)
            {
                foreach (var VARIABLE in instancePlanet.Roots)
                {
                    VARIABLE.wasUsedInThisVisit = false;
                }
                //instancePlanet.lastVisitedTime++;
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
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (player.GetButtonDown(RewiredConsts.Action.Plant))
        {
            if (curr.Roots.Count <= 2)
            {
                animator.SetTrigger("Plant");
                var temp = Instantiate(basePrefab, piedi.position, transform.rotation);
                temp.GetComponentInChildren<MeshRenderer>().material.SetFloat("_Radius",
                    Vector3.Distance(piedi.position, curr.transform.position));
                StartCoroutine(temp.GetComponentInChildren<Root>().RootAnimation(0.4f));
                temp.GetComponentInChildren<Root>().planetWhereIsPlanted = curr;
                temp.GetComponentInChildren<Root>().setHeights();
                curr.Roots.Add(temp.GetComponentInChildren<Root>());
                foreach (var instancePlanet in PlanetsManager.Instance.planets)
                {
                    if (instancePlanet != curr)
                    {
                        foreach (var VARIABLE in instancePlanet.Roots)
                        {
                            VARIABLE.Decrease();
                        }
                        //instancePlanet.lastVisitedTime++;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Root>())
        {
            var temp = other.GetComponentInChildren<Root>();
            Debug.Log("plant!");
            if (!temp.wasUsedInThisVisit)
            {
                StartCoroutine(temp.RootAnimation(temp.currGrowth+temp.howMuchGrows));
            }
        }
    }
}