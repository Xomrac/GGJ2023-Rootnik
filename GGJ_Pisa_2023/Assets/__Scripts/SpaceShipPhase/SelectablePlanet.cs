using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public enum Fertility
{

}
public class SelectablePlanet : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private float distance;
    [ReadOnly][SerializeField] private float lastVisitTime;
    [SerializeField] private string coordinates;
    [SerializeField] private Fertility planetFertility;
    [SerializeField] private float timeToReach;
    public float TimeToReach => timeToReach;

    [SerializeField] private Sprite planetIcon;
    [SerializeField] public Transform focusPoint;


    public static Action<SelectablePlanet> OnPlanetClicked;

    private void OnMouseDown()
    {
        OnPlanetClicked?.Invoke(this);
    }
}
