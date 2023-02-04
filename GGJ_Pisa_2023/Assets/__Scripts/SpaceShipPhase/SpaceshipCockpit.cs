using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Jam
{

	[RequireComponent(typeof(BoxCollider))]
	public class SpaceshipCockpit : MonoBehaviour
	{
		public static event Action onEnteringPoint;
		[ReadOnly] public bool entered = false;

		private void OnTriggerEnter(Collider other)
		{
			if (entered) return;
			if (other.CompareTag("Player"))
			{
				onEnteringPoint?.Invoke();
				
				entered = true;
			}
		}
	}

}