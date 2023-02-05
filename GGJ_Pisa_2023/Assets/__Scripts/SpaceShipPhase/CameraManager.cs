using System;
using System.Collections;
using Cinemachine;
using Riutilizzabile;
using UnityEditor.UIElements;
using UnityEngine;

namespace Jam
{

	public class CameraManager : Singleton<CameraManager>
	{
		[SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
		[SerializeField] private Vector3 startBodyVector3;
		[SerializeField] private Vector3 levelSelectionBodyValue;
		[SerializeField] private Vector3 startRot;
		[SerializeField] private Vector3 mapRot;

		[SerializeField] private TopdownController player;

		private void OnEnable()
		{
			LevelSelectionManager.OnLeavingLevelSelection += FocusPlayer;
			
		}

		private void OnDisable()
		{
			LevelSelectionManager.OnLeavingLevelSelection -= FocusPlayer;

		}

		private void Start()
		{
			startBodyVector3 = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
			startRot = cinemachineVirtualCamera.transform.eulerAngles;
		}
		
		public void ChangeOffsetToMap()
		{
			cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = levelSelectionBodyValue;
			cinemachineVirtualCamera.transform.eulerAngles = mapRot;
		}

		public void ChangeoffsetToPlayer()
		{
			cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = startBodyVector3;
			cinemachineVirtualCamera.transform.eulerAngles = startRot;
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