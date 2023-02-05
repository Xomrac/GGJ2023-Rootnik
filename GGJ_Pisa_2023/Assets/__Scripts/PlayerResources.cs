using Jam.General;
using UnityEngine;

namespace Jam
{

	public static class PlayerResources
	{
		public static float currentFood;
		public static float currentWater;
		public static float currentOxygen;
		public static float maxFood=200;
		public static float MaxOxygen=100;
		public static float Maxwater = 150;

		public static float deepestRootPlanetDistance;

		public static float currentPlanetDistance;

		public static void modifyFood(float val)
		{
			currentFood = Mathf.Clamp(currentFood+val, 0, maxFood);
			if (currentFood==20)
			{
				AudioManaegr.Instance.PlayFx("DangerPlayer0");
			}
		}
		public static void modifyWater(float val)
		{
			currentFood = Mathf.Clamp(currentWater+val, 0, Maxwater);
			if (currentWater==15)
			{
				AudioManaegr.Instance.PlayFx("DangerPlayer0");
			}
		}
		public static void modifyOxygen(float val)
		{
			currentFood = Mathf.Clamp(currentOxygen+val, 0, MaxOxygen);
			if (MaxOxygen==10)
			{
				AudioManaegr.Instance.PlayFx("DangerPlayer0");
			}
		}
		public static float DistanceFromDeepest => Mathf.Abs(deepestRootPlanetDistance - currentPlanetDistance);
	}

}