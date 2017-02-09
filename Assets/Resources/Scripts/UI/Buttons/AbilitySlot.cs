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

	private GameObject popupInfo;

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
		//TODO ability info popup
		//maybe have a time delay between mouse over and display
		Debug.Log ("Ability info popup should appear now.");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log ("Ability popup info should disappear");
	}
}
