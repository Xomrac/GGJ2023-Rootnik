using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public enum layers
{
    Layer1,
    Layer2,
    Layer3,
    nucleo
    
}
public struct PondMinAndObjects
{
    public int min;
    public List<GameObject> pondsObj;
}

[Serializable]
public class PlanetStats: SerializedMonoBehaviour , ISavable
{
    public Fertility fert;
    [FoldoutGroup("don't touch")]
    public float lastVisitedTime;
    [FoldoutGroup("don't touch")]
    public layers maxReached;
    [FoldoutGroup("don't touch")]
    public string UID;
    public Transform landingPoint;
    public Dictionary<layers, PondMinAndObjects> pondsValue;
    public Dictionary<layers, int> gainValue;
    [FoldoutGroup("don't touch")]
    public List<Root> Roots;
    [FoldoutGroup("don't touch")]
    public float radiusLenght2;
    [FoldoutGroup("don't touch")]
    public float radiusLenght3;
    [FoldoutGroup("don't touch")]
    public float radiusnucleo;

    public bool wasVisited;

    //si guadagna max maggiore metà minore
    

    public void SpawnPonds()
    {
        foreach (var layer  in pondsValue)
        {
            float percent =100/( layer.Value.pondsObj.Count - layer.Value.min);
            for (int i = 0; i < layer.Value.pondsObj.Count; i++)
            {
                if (i<layer.Value.min)
                {
                    layer.Value.pondsObj[i].SetActive(true);
                }
                else
                {
                    float rand = Random.Range(0, 101);
                    if (rand<percent/i)
                    {
                        layer.Value.pondsObj[i].SetActive(true);
                    }
               
                }
            }
        }
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnPonds();
        }
    }

    public void OnSave()
    {
        
    }
}
