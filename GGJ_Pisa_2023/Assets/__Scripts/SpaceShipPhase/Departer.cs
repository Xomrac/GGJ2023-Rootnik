using Riutilizzabile;
using UnityEngine;

namespace Jam
{

	public class Departer : Singleton<Departer>
	{

		[SerializeField] private Vector3 playerPos;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				other.gameObject.transform.position = playerPos;
				other.gameObject.SetActive(false);
				Exit();
			}
		}
		public void Exit()
		{
			var player = FindObjectOfType<TopdownController>();
			player.transform.position = playerPos;
			player.gameObject.SetActive(false);
			LevelManager.Instance.ChangeScreen(GameState.Roaming);
			GameManager.Instance.planetCharacter.gameObject.SetActive(true);
			GameManager.Instance.planetCharacter.OnLand(PlanetsConnector.Instance.roamingPlanets[TravelManager.Instance.planetChosen]);
		}
	}

}