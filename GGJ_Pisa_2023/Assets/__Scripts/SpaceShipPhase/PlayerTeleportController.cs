using UnityEngine;

namespace Jam
{

	public class PlayerTeleportController : MonoBehaviour
	{
		[SerializeField] public Transform currentDestination;

		public bool CanTeleport => currentDestination == null;
	}

}