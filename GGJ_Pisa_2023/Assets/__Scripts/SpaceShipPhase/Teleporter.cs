using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Jam
{

	[RequireComponent(typeof(BoxCollider))]
	public class Teleporter : MonoBehaviour
	{
		[SerializeField] private Transform destination;
		[SerializeField] private PlayerTeleportController player;

		private bool teleporting;

		 public bool entered=false;

		private void Start()
		{
			player = FindObjectOfType<PlayerTeleportController>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (entered ) return;
			if (other.CompareTag("Player"))
			{
				if (!player.CanTeleport) return;
				player.currentDestination = destination;
				FadeController.Instance.CompleteFade();
				Debug.Log("Entered!");
				Teleport();
				entered = true;
			}
		}

		private void Teleport()
		{
			teleporting = true;
			StopAllCoroutines();
			StartCoroutine(Waiter());
			IEnumerator Waiter()
			{
				yield return new WaitForSeconds(FadeController.Instance.HalfFadeTime);
				player.transform.position = destination.position;
				yield return new WaitForSeconds(1f);
				teleporting = false;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player") && !teleporting)
			{
				entered = false;
				player.currentDestination = null;
			}
		}
	}

}