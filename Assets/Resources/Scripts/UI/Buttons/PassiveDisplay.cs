using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PassiveDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	//TODO popup info prefab to instantiate and display passive info

	public int passiveIndex;

	public void OnPointerEnter(PointerEventData data)
	{
		//TODO create popup
	}

	public void OnPointerExit(PointerEventData data)
	{
		//TODO destroy popup
	}
}
