using UnityEngine;

namespace Jam
{

	public static class Utilities
	{
		public static void Set(this CanvasGroup canvasGroup, float alpha, bool interactable, bool blocksRaycasts)
		{
			canvasGroup.alpha = alpha;
			canvasGroup.blocksRaycasts = blocksRaycasts;
			canvasGroup.interactable = interactable;
		}

	}

}