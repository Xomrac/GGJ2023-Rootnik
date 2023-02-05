using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoolCustomButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
	{
		public event Action pointerEntered;
		public event Action pointerExited;
		

		public void OnPointerEnter(PointerEventData eventData)
		{
			pointerEntered?.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			pointerExited?.Invoke();
		}
	}
