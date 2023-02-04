using System;
using System.Collections;
using Cinemachine;
using Riutilizzabile;
using UnityEngine;

namespace Jam
{

	public class CameraManager : Singleton<CameraManager>
	{
		[SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

		[SerializeField] private TopdownController player;

		private void OnEnable()
		{
			LevelSelectionManager.OnLeavingLevelSelection += FocusPlayer;
			
		}

		private void OnDisable()
		{
			LevelSelectionManager.OnLeavingLevelSelection -= FocusPlayer;

		}

		private void FocusPlayer()
		{
			StartCoroutine(Waiter());
			IEnumerator Waiter()
			{
				yield return new WaitForSeconds((FadeController.Instance.HalfFadeTime));
				cinemachineVirtualCamera.Follow = player.transform;
				cinemachineVirtualCamera.LookAt = player.transform;
			}
			
		}
	}

}