using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Jam
{

	public class LevelSelectionManager : MonoBehaviour
	{
		public static Action OnLeavingLevelSelection;
		[SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
		[SerializeField] private PlanetInfoDisplayer planetInfoDisplayer;
		[SerializeField] private Transform defaultLookPoint;
		[SerializeField] private CanvasGroup canvasGroup;

		private void OnEnable()
		{
			SelectablePlanet.OnPlanetClicked += ChnageCameraFocus;
			PlanetInfoDisplayer.onPlanetUnfocused += FocusDefaultPointInstant;
		}

		private void OnDisable()
		{
			SelectablePlanet.OnPlanetClicked -= ChnageCameraFocus;
			PlanetInfoDisplayer.onPlanetUnfocused -= FocusDefaultPointInstant;
		}

		private void Start()
		{
			canvasGroup.Set(0,false,false);
		}

		private void ChnageCameraFocus(SelectablePlanet planet)
		{
			cinemachineVirtualCamera.Follow = planet.focusPoint.transform;
			cinemachineVirtualCamera.LookAt = planet.focusPoint.transform;
			planetInfoDisplayer.DisplayInfos(planet);
			canvasGroup.Set(0,false,false);
		}

		public void FocusDefaultPointDelayed()
		{
			StartCoroutine(Waiter());
			IEnumerator Waiter()
			{
				yield return new WaitForSeconds(FadeController.Instance.HalfFadeTime);
				cinemachineVirtualCamera.Follow = defaultLookPoint;
				cinemachineVirtualCamera.LookAt = defaultLookPoint;
				canvasGroup.Set(1, true, true);
			}
			
		}
		public void FocusDefaultPointInstant()
		{
			cinemachineVirtualCamera.Follow = defaultLookPoint;
				cinemachineVirtualCamera.LookAt = defaultLookPoint;
				canvasGroup.Set(1, true, true);
		}

		public void GoBack()
		{
			canvasGroup.Set(0,false,false);
			OnLeavingLevelSelection?.Invoke();
		}
	}

}