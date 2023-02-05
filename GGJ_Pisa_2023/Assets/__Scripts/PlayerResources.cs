using UnityEngine;

namespace Jam
{

	public static class PlayerResources
	{
		public static float currentFood;
		public static float currentWater;
		public static float currentOxygen;

		public static float deepestRootPlanetDistance;

		public static float currentPlanetDistance;

		public static float DistanceFromDeepest => Mathf.Abs(deepestRootPlanetDistance - currentPlanetDistance);
	}

}