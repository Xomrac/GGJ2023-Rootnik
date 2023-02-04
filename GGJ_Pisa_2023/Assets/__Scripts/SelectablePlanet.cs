using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SelectablePlanet : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private float distance;
    [ReadOnly][SerializeField] private float lastVisitTime;
}
