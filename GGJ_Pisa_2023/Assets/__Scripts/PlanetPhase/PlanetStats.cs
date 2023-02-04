using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public enum layers
{
    Layer1,
    Layer2,
    Layer3
    
}

[Serializable]
public class PlanetStats: SerializedMonoBehaviour , ISavable
{
    public int lastVisitedTime;
    public Dictionary<layers, int> gainValue;
    public string UID;
    public Transform landingPoint;

    private void Start()
    {
        landingPoint = gameObject.GetComponentInChildren<Transform>();
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (UID == "")
        {
            UID = GUID.Generate().ToString();
            EditorUtility.SetDirty(this);
        }
#endif
    }

    public void OnSave()
    {
        
    }
}
