using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using Sirenix.OdinInspector;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Serialization;


public class Root : MonoBehaviour
{
    public float howMuchGrows;
    public float howMuchDecres;
    public float GainDecreseAfterNucleo;
    public float GainIncreaseAfterNucleoRecover;
    public float timeAnim;
    [FoldoutGroup("don't touch")] public bool wasUsedInThisVisit = false;
    [FoldoutGroup("don't touch")] public PlanetStats planetWhereIsPlanted;
    [FoldoutGroup("don't touch")] public layers layer = layers.Layer1;
    [FoldoutGroup("don't touch")] public float currGrowth;
    [FoldoutGroup("don't touch")] public float maxRadius;
    [FoldoutGroup("don't touch")] public float HeightForLayer3;
    [FoldoutGroup("don't touch")] public float HeightForLayer2;
    [FoldoutGroup("don't touch")] public float HeightFornucleo;
    [FoldoutGroup("don't touch")] public float MaxGain;
    [FoldoutGroup("don't touch")] public float currGain;
    [FoldoutGroup("don't touch")] public bool NucleoTouched;

    public List<GameObject> poolInRange;

    public void setHeights()
    {
        HeightForLayer2 = maxRadius - planetWhereIsPlanted.radiusLenght3;
        HeightForLayer3 = maxRadius - planetWhereIsPlanted.radiusLenght2;
        HeightFornucleo = maxRadius - planetWhereIsPlanted.radiusnucleo;
    }

    public void CollectResource()
    {
        wasUsedInThisVisit = true;
        if (layer == layers.nucleo)
        {
            if (NucleoTouched)
            {
                currGain = Mathf.Clamp(currGain - GainDecreseAfterNucleo, 0, MaxGain);
            }
            else
            {
                MaxGain = planetWhereIsPlanted.gainValue[layer];
                currGain = MaxGain;
                NucleoTouched = true;
            }

            PlayerResources.currentFood += currGain;
        }
        else
        {
            if (!NucleoTouched)
            {
                if (planetWhereIsPlanted.Roots.IndexOf(this) != 0)
                {
                    PlayerResources.currentFood += planetWhereIsPlanted.gainValue[layer] / 2f;
                }
                else
                {
                    PlayerResources.currentFood += planetWhereIsPlanted.gainValue[layer];
                }
            }
            else
            {
                if (planetWhereIsPlanted.Roots.IndexOf(this) != 0)
                {
                    PlayerResources.currentFood += currGain / 2f;
                }
                else
                {
                    PlayerResources.currentFood += currGain;
                }
            }
        }
        collectFromPond();
    }

    public void collectFromPond()
    {
        
        for(var i = poolInRange.Count - 1; i > -1; i--)
        {
            if (poolInRange[i] == null)
                poolInRange.RemoveAt(i);
        }
        foreach (var pond in poolInRange)
        {
            
            if (planetWhereIsPlanted.pondsValue[layers.Layer1].pondsObj.Contains(pond))
            {
                PlayerResources.currentWater+=planetWhereIsPlanted.gainValue[layers.Layer1];
            }

            if (planetWhereIsPlanted.pondsValue[layers.Layer2].pondsObj.Contains(pond))
            {
                PlayerResources.currentWater+=planetWhereIsPlanted.gainValue[layers.Layer2];
            }
            if (planetWhereIsPlanted.pondsValue[layers.Layer3].pondsObj.Contains(pond))
            {
                PlayerResources.currentWater+=planetWhereIsPlanted.gainValue[layers.Layer3];
            }
            var temp = pond.GetComponent<Pond>();
            temp.numberOfTimeUsed++;
            temp.checkIfEmpty();
        }
    }
    public void Decrease()
    {
        if (currGrowth - howMuchDecres <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            if (NucleoTouched)
            {
                currGain = Mathf.Clamp(currGain + GainIncreaseAfterNucleoRecover, 0, MaxGain);
            }

            currGrowth -= howMuchDecres;
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Height", currGrowth);
        }

        CheckInWichLayer();
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, (currGrowth * 39) / 100,
            GetComponent<BoxCollider>().size.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Pond"))
        {
            poolInRange.Add(other.gameObject);
            other.GetComponent<Pond>().TRoot = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            poolInRange.Remove(other.gameObject);
            other.GetComponent<Pond>().TRoot = null;
        }
    }

    public IEnumerator RootAnimation(float howMuchToGrow)
    {
        float elapsedTime = 0;
        float val = 0;
        float dioMaiale = GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");

        while (elapsedTime < timeAnim)
        {
            elapsedTime += Time.deltaTime;
            val = Mathf.Lerp(dioMaiale, howMuchToGrow, elapsedTime / timeAnim);
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Height", val);
            currGrowth = GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, (currGrowth * 39) / 100, GetComponent<BoxCollider>().size.z);
            yield return null;
        }
        currGrowth = GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");
        CheckInWichLayer();
        CollectResource();
    }

    public void CheckInWichLayer()
    {
        if (currGrowth > HeightForLayer2)
        {
            layer = layers.Layer2;
        }

        if (currGrowth > HeightForLayer3)
        {
            layer = layers.Layer3;
        }

        if (currGrowth > HeightFornucleo)
        {
            layer = layers.nucleo;
        }
    }
}