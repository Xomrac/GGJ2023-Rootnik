using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuController : SerializedMonoBehaviour
{
    public Dictionary<ResourceType, Sprite> resourcesSprites;


    [SerializeField] private List<Button> buttons;

    private void SetupButtons(List<ResourceType> types)
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
