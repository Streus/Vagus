using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatusSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Status status;

	public Image statusIcon;
	public Text statusCool;

	private GameObject popupInfo;

	public void Start()
	{
		if (status != null)
			statusIcon.sprite = status.Icon;
	}

	public void Update()
	{
		if (status == null)
			return;
			
		if (status.DurationRem <= 0f)
		{
			status = null;
			Destroy (gameObject);
		}

		statusCool.text = status.DurationRem.ToString ("###");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//TODO status popup info
		//maybe have a time delay between mouse over and display
		Debug.Log ("Status popup info should appear now");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log ("Status popup info should disappear now");
	}
}
