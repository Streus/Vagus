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
		GameObject pu = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/Popup Info"));
		pu.transform.SetParent (transform, false);
		popup = pu.GetComponent<PopupInfo> ();
		popup.changeText (thisPass.ToString ());
		Debug.Log("A totally cool popup should appear right about now!");
	}

	public void OnPointerExit(PointerEventData data)
	{
		Destroy (popup.gameObject);
		Debug.Log("Lame. The popup is gone.");
	}
}
