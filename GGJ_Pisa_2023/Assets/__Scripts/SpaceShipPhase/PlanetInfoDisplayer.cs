using System;
using UnityEngine;

namespace Jam
{

	public class PlanetInfoDisplayer : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;
		public static Action onPlanetUnfocused;
		[SerializeField] private SelectablePlanet selectedPlanet;
		[SerializeField] private LevelSelectionManager levelSelectionManager;


		private void Start()
		{
			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		public void DisplayInfos(SelectablePlanet planet)
		{
			canvasGroup.Set(1,true,true);
			selectedPlanet = planet;
		}

		public void UnFocus()
		{
			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			onPlanetUnfocused?.Invoke();
		}


		public void Depart()
		{
			TravelManager.Instance.StartVoyaje(selectedPlanet.TimeToReach);
			TravelManager.Instance.planetChosen = selectedPlanet.index;
			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			Departer.Instance.Exit();
		}
		
	}

}