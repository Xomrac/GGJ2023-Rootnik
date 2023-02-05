using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenuController : SerializedMonoBehaviour
{
	public Dictionary<ResourceType, Sprite> resourcesSprites;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private ShipResourcesManager resourcesManager;
	[SerializeField] private float fadeTime;

	[SerializeField] private List<Button> buttons;
	private Coroutine fadeCoroutine;

	private void Start()
	{
		resourcesManager = FindObjectOfType<ShipResourcesManager>();
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		gameObject.SetActive(resourcesManager!=null);
	}

	private void OnEnable()
	{
		ShipInteractionPoint.onEnteringPoint += SetupButtons;
		ShipInteractionPoint.onExitPoint += TurnOffButtons;
	}
	private void OnDisable()
	{
		ShipInteractionPoint.onEnteringPoint -= SetupButtons;
		ShipInteractionPoint.onExitPoint -= TurnOffButtons;
	}

	private void SetupButtons(Dictionary<ResourceType,float> types)
	{
		StopAllCoroutines();
		TurnOffButtons();
		StartCoroutine(FadeInCoroutine());
		IEnumerator FadeInCoroutine()
		{
			float elapsedTime = 0;
			while (elapsedTime<fadeTime)
			{
				canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			canvasGroup.alpha = 1;
		}
		int index = 0;
		foreach (KeyValuePair<ResourceType, float> keyValuePair in types)
		{
			buttons[index].gameObject.SetActive(true);
			buttons[index].onClick.RemoveAllListeners();
			buttons[index].GetComponent<Image>().sprite = resourcesSprites[keyValuePair.Key];
			buttons[index].GetComponent<CoolCustomButton>().pointerEntered += () => { resourcesManager.StartConsume(keyValuePair.Key, keyValuePair.Value);};
			buttons[index].GetComponent<CoolCustomButton>().pointerExited += () => { resourcesManager.StopConsume();};
			// buttons[index].onClick.AddListener(TurnOffButtons);
			// buttons[index].onClick.AddListener(() =>{resourcesManager.StartConsume(keyValuePair.Key, keyValuePair.Value);});
			index++;
		}
	}



	private void TurnOffButtons()
	{
		StartCoroutine(FadeOutCoroutine());
		IEnumerator FadeOutCoroutine()
		{
			float elapsedTime = 0;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			while (elapsedTime<fadeTime)
			{
				canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			canvasGroup.alpha = 0;
		}
	}
}
