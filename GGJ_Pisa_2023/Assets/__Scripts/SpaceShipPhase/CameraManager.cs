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
		[SerializeField] private CinemachineVirtualCamera camera;
		[SerializeField] private Vector3 spaceshipBody;
		[SerializeField] private Vector3 spaceshipRotation;
		[SerializeField] private Vector3 levelSelectionBody;
		[SerializeField] private Vector3 levelSelectionRotation;
		[SerializeField] private Vector3 roamingBody;
		[SerializeField] private Vector3 roamingRotation;
		[SerializeField] private Transform spaceshipFocus;
		[SerializeField] private Transform levelSelectionFocus;
		[SerializeField] private Transform planetRoamingFocus;
		[SerializeField] private float normalFOV;
		[SerializeField] private float superFOV;

		private void Start()
		{
			setupCamera(GameState.Spaceship);
		}

		public void setupCamera(GameState gameState)
		{
			switch (gameState)
			{
				case GameState.Spaceship:
					camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = spaceshipBody;
					camera.m_Lens.FieldOfView = normalFOV;
					camera.Follow = spaceshipFocus;
					camera.LookAt = spaceshipFocus;
					camera.transform.eulerAngles = spaceshipRotation;
					break;
				case GameState.LevelSelection:
					camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = levelSelectionBody;
					camera.Follow = levelSelectionFocus;
					camera.m_Lens.FieldOfView = normalFOV;
					camera.LookAt = levelSelectionFocus;
					camera.transform.eulerAngles = levelSelectionRotation;
					break;
				case GameState.Roaming:
					camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = roamingBody;
					camera.Follow = planetRoamingFocus;
					camera.m_Lens.FieldOfView = superFOV;
					camera.LookAt = planetRoamingFocus;
					camera.transform.eulerAngles = roamingRotation;
					break;
			}
		}
	}

}