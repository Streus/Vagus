using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Ability ability;

	public Image abilityIcon;
	public Image coolOverlay;
	public Text coolText;
	public GameObject chargesIndicator;
	public Text chargesText;

	private PopupInfo popupInfo;
	private bool mouseHovering;

	public void Start()
	{
		chargesIndicator.SetActive (false);
	}

	public void Update()
	{
		if (ability == null)
		{
			abilityIcon.sprite = null;

			coolOverlay.fillAmount = 0f;
			coolText.text = "";

			chargesIndicator.SetActive (false);
		}
		else
		{
			abilityIcon.sprite = ability.Icon;

			coolOverlay.fillAmount = ability.CooldownCurr / ability.CooldownMax;
			coolText.text = ability.CooldownCurr.ToString ("###");

			chargesIndicator.SetActive (ability.ChargesMax > 0);
			if (chargesIndicator.activeSelf)
			{
				chargesText.text = ability.ChargesCurr.ToString ("###");
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseHovering = true;
		Invoke ("showPopup", 1f);
	}

	private void showPopup()
	{
		if (mouseHovering)
		{
			string text;
			if (ability != null)
				text = ability.ToString ();
			else
				text = "This slot is empty!";
			Canvas canvas = transform.root.GetComponent<Canvas> ().rootCanvas;
			popupInfo = PopupInfo.createPopup (text, canvas, transform.position);

		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseHovering = false;
		if (popupInfo != null)
			Destroy (popupInfo.gameObject);
	}
}
