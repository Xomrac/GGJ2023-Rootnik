using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using Riutilizzabile;
using UnityEngine;

public enum GameState
{
	Spaceship,
	LevelSelection,
	Roaming

}

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private GameObject shipGameobject;
	[SerializeField] private Camera camera;
	[SerializeField] private GameObject stellarMapGameObject;
	[SerializeField] private GameObject planetRoamingGameObject;

	[SerializeField] public GameState currentSection;

	
	
	private void Start()
	{
		planetRoamingGameObject.SetActive(false);
		planetRoamingGameObject.SetActive(false);
		// camera=Camera.main;
	}

	public void ChangeScreen(GameState nextgamestate)
	{
		StartCoroutine(ChangeScreenCoroutine());

		IEnumerator ChangeScreenCoroutine()
		{
			FadeController.Instance.CompleteFade();
			yield return new WaitForSeconds(FadeController.Instance.HalfFadeTime);
			
			switch (currentSection)
			{
				case GameState.Spaceship:
					shipGameobject.SetActive(false);
					break;
				case GameState.LevelSelection:
					stellarMapGameObject.SetActive(false);
					break;
				case GameState.Roaming:
					planetRoamingGameObject.SetActive(false);
					break;
			}
			currentSection = nextgamestate;
			switch (currentSection)
			{
				case GameState.Spaceship:
					camera.clearFlags=CameraClearFlags.Skybox;
					shipGameobject.SetActive(true);
					break;
				case GameState.LevelSelection:
					camera.clearFlags=CameraClearFlags.SolidColor;
					stellarMapGameObject.SetActive(true);
					break;
				case GameState.Roaming:
					camera.clearFlags=CameraClearFlags.Skybox;
					planetRoamingGameObject.SetActive(true);
					break;
			}
			CameraManager.Instance.setupCamera(currentSection);
		}
	}
}