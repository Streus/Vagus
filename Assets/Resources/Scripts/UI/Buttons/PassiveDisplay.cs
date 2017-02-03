using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PassiveDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	//TODO popup info prefab to instantiate and display passive info

	public Image displayIcon;
	private Passive thisPass;

	public void changePassive(Passive p)
	{
		thisPass = p;
		if(thisPass != null)
			displayIcon.sprite = thisPass.Icon;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		//TODO create popup
		Debug.Log("A totally cool popup should appear right about now!");
	}

	public void OnPointerExit(PointerEventData data)
	{
		//TODO destroy popup
		Debug.Log("Lame. The popup is gone.");
	}
}
