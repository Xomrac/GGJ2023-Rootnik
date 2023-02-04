using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetObject : MonoBehaviour
{
    public string UID;
    public PlanetStats stats=new PlanetStats();
    
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
