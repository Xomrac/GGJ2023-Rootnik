using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public enum Fertility
{
    Fertile,
    NotSoFertile,
    Barren,
    Unfertile
}
public class SelectablePlanet : MonoBehaviour
{
    [SerializeField] public string displayName;
    [SerializeField] public float distance;
    [ReadOnly][SerializeField] private float lastVisitTime;
    [SerializeField] private string coordinates;
    [SerializeField] public Fertility planetFertility;
    [SerializeField] private float timeToReach;
    public float TimeToReach => timeToReach;
    public GameObject rootFeedback;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer misterySprite;
    public int index;
    [SerializeField] private Sprite planetIcon;
    [SerializeField] public Transform focusPoint;


    public static Action<SelectablePlanet> OnPlanetClicked;

    private void OnMouseDown()
    {
        OnPlanetClicked?.Invoke(this);
    }
}
