using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] private GameObject shipGameobject;
   [SerializeField] private GameObject stellarMapGameObject;

   private void OnEnable()
   {
      SpaceshipCockpit.onEnteringPoint += ChangeScreen;
      LevelSelectionManager.OnLeavingLevelSelection += ChangeScreen;
   }

   private void OnDisable()
   {
      SpaceshipCockpit.onEnteringPoint -= ChangeScreen;
      LevelSelectionManager.OnLeavingLevelSelection -= ChangeScreen;
   }

   private void ChangeScreen()
   {
      StartCoroutine(ChangeScreenCoroutine());

      IEnumerator ChangeScreenCoroutine()
      {
         FadeController.Instance.CompleteFade();
         yield return new WaitForSeconds(FadeController.Instance.HalfFadeTime);
         shipGameobject.SetActive(!shipGameobject.activeSelf);
         stellarMapGameObject.SetActive(!stellarMapGameObject.activeSelf);
      }
      
   }
}
