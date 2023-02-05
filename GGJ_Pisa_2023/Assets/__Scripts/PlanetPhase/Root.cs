using System;
using System.Collections;
using Jam;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Serialization;


public class Root : MonoBehaviour
{
    public float timeAnim;
    public bool wasUsedInThisVisit = false;
    public PlanetStats planetWhereIsPlanted;
    public layers layer = layers.Layer1;
    public float howMuchGrows;
    public float howMuchDecres;
    public float currGrowth;
    public float maxRadius;
    public float HeightForLayer3;
    public float HeightForLayer2;

    public void setHeights()
    {
        HeightForLayer2 = maxRadius - planetWhereIsPlanted.radiusLenght3;
        HeightForLayer3 = maxRadius - planetWhereIsPlanted.radiusLenght2;
    }

    public void CollectResource()
    {
        wasUsedInThisVisit = true;
        if (GetComponent<MeshRenderer>().material.GetFloat("_Height") >= maxRadius)
        {
            Debug.Log("maxReached");
        }
        else
        {
            if (planetWhereIsPlanted.Roots.IndexOf(this) != 0)
            {
                PlayerResources.currentFood += planetWhereIsPlanted.gainValue[layer] / 2f;
                Debug.Log(PlayerResources.currentFood + " half");
            }
            else
            {
                PlayerResources.currentFood += planetWhereIsPlanted.gainValue[layer];
                Debug.Log(PlayerResources.currentFood);
            }
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
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Height", currGrowth - howMuchDecres);
            currGrowth -= howMuchDecres;
        }
    }


    public IEnumerator RootAnimation(float howMuchToGrow)
    {
        float elapsedTime = 0;
        float val = 0;
        float dioMaiale=GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");

        while (elapsedTime < timeAnim)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            val = Mathf.Lerp(dioMaiale, howMuchToGrow, elapsedTime / timeAnim);
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Height", val);
            currGrowth = GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");
            yield return null;
        }

        currGrowth = GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");
        if (currGrowth > HeightForLayer2)
        {
            layer = layers.Layer2;
        }
        if (currGrowth > HeightForLayer3)
        {
            layer = layers.Layer3;
        }


        CollectResource();
    }
}