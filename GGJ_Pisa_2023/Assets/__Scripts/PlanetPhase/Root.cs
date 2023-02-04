using System;
using System.Collections;
using UnityEngine;


public class Root : MonoBehaviour
{
    public float timeAnim;
    public bool wasUsedInThisVisit = false;
    public PlanetStats planetWhereIsPlanted;
    public layers layer=layers.Layer1;
    public float howMuchGrows;
    public float howMuchDecres;
    public float currGrowth;
    public float maxRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Layer1"))
        {
            layer = layers.Layer2;
        }

        if (other.CompareTag("Layer2"))
        {
            layer = layers.Layer3;
        }
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
            Debug.Log(planetWhereIsPlanted.gainValue[layer]);
            
        }
    }

    public void Decrease()
    {
        if (currGrowth-howMuchDecres<=0)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Height",currGrowth-howMuchDecres);
            currGrowth -= howMuchDecres; 
        }
        
    }


    public IEnumerator RootAnimation(float howMuchToGrow)
    {
        float elapsed = 0;
        float val = 0;
        while (elapsed < timeAnim)
        {
            elapsed += Time.deltaTime;
            val = Mathf.Lerp(0, howMuchToGrow, elapsed/timeAnim);
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Height", val);
            yield return null;
        }

        currGrowth = GetComponentInChildren<MeshRenderer>().material.GetFloat("_Height");
        maxRadius= GetComponentInChildren<MeshRenderer>().material.GetFloat("_Radius");
        CollectResource();
    }
}