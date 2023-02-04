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
    Layer3
    
}
public struct PondMinAndObjects
{
    public int min;
    public List<GameObject> pondsObj;
}

[Serializable]
public class PlanetStats: SerializedMonoBehaviour , ISavable
{
    public float lastVisitedTime;
    public int rootsPlanted;
    public layers maxReached;
    public string UID;
    public Transform landingPoint;
    public Dictionary<layers, PondMinAndObjects> pondsValue;
    public Dictionary<layers, int> gainValue;
    public List<Root> Roots;

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
                    Debug.Log(i);
                }
                else
                {
                    float rand = Random.Range(0, 101);
                    Debug.Log(layer.Value.pondsObj[i].name+" not spawned "+rand+" "+percent/i);
                    if (rand<percent/i)
                    {
                        Debug.Log(layer.Value.pondsObj[i].name+" "+rand+" "+percent/i);
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
