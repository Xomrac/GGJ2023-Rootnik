using System;
using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;
using UnityEngine.Accessibility;

public class SkyboxManager : Singleton<SkyboxManager>
{
    [SerializeField] private Material skyboxMaterial;

    private void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
    }


    public void RemoveSkybox()
    {
        Debug.Log("skybox removed");
        RenderSettings.skybox = null;
    }

    public void PlaceSkybox()
    {
        RenderSettings.skybox = skyboxMaterial;
    }
}
