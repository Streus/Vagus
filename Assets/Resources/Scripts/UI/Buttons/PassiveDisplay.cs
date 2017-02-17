using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PassiveDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Image displayIcon;
	private Passive thisPass;

	private PopupInfo popup;

	public void changePassive(Passive p)
	{
		thisPass = p;
		if(thisPass != null)
			displayIcon.sprite = thisPass.Icon;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		Canvas canvas = transform.root.GetComponent<Canvas> ().rootCanvas;
		popup = PopupInfo.createPopup (thisPass.ToString (), canvas, transform.position);
	}

	public void OnPointerExit(PointerEventData data)
	{
		Destroy (popup.gameObject);
	}
}
