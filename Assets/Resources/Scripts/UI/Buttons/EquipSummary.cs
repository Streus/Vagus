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
			displays [i].changePassive(PlayerPerks.passives[i]);
		}
	}

	public void changePassive(int index, Passive p)
	{
		if (index < PlayerPerks.passives.Length)
		{
			PlayerPerks.passives [index] = p;
			displays [index].changePassive (p);
			OnSlotChanged (index);
		}
	}

	public void swapPassives(int index1, int index2)
	{
		PlayerPerks.swapPassives (index1, index2);
		displays [index1].changePassive(PlayerPerks.passives [index1]);
		displays [index2].changePassive(PlayerPerks.passives [index2]);
	}

	public void savePassives()
	{
		PlayerPerks.savePassives ();
	}

	public event ChangedSlotPassive slotChanged;
	public void OnSlotChanged(int slotNumber)
	{
		if (slotChanged != null)
			slotChanged (slotNumber);
	}
}
