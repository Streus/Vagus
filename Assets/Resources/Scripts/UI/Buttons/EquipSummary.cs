using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public delegate void ChangedSlotPassive(int slotNumber);

public class EquipSummary : MonoBehaviour
{
	private PassiveDisplay[] displays;

	public void Start()
	{
		displays = new PassiveDisplay[4];
		for(int i = 0; i < displays.Length; i++)
		{
			displays [i] = transform.GetChild (i).GetComponent<PassiveDisplay> ();
			displays [i].passiveIndex = i;
		}
	}

	public void changePassive(int index, Passive p)
	{
		if (index < PlayerPerks.passives.Length)
			PlayerPerks.passives [index] = p;
		OnSlotChanged (index);
	}

	public event ChangedSlotPassive slotChanged;
	public void OnSlotChanged(int slotNumber)
	{
		if (slotChanged != null)
			slotChanged (slotNumber);
	}
}
