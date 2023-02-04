using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using Sirenix.OdinInspector;
using UnityEngine;

public enum FadeType
{
    In,
    Out
}
public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime;
    [SerializeField] private float timeBetweenFades;

    private void Awake()
    {
        canvasGroup.Set(1,true,true);
        StartCoroutine(FadeCoroutine(FadeType.Out));
    }

    [Button]
    private void StopFade()
    {
        StopAllCoroutines();
        canvasGroup.Set(0,false,false);
    }
    [Button]
    private void Fade(FadeType fadeType)
    {
        StartCoroutine(FadeCoroutine(fadeType));
    }
    
    private IEnumerator FadeCoroutine(FadeType fadeType)
    {
        float elapsedTime = 0;
        while (elapsedTime<fadeTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = fadeType == FadeType.In ? Mathf.Lerp(0, 1, elapsedTime / fadeTime) : Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            yield return null;
        }
        switch (fadeType)
        {
            case FadeType.In:
                canvasGroup.Set(1,true,true);
                break;
            case FadeType.Out:
                canvasGroup.Set(0,false,false);
                break;
        }
    }

[Button]
    private void CompleteFade()
    {
        StartCoroutine(completeFadeCoroutine());
        IEnumerator completeFadeCoroutine()
        {
            yield return StartCoroutine(FadeCoroutine(FadeType.In));
            yield return new WaitForSeconds(timeBetweenFades);
            StartCoroutine(FadeCoroutine(FadeType.Out));
        }
    }
    
    
    
}
