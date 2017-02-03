using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PerkDescription : MonoBehaviour
{
	public EquipSummary summary;
	private ChangedSlotPassive thisSlot;

	public string perkName;
	private Passive perk;

	public Text perkDescription;
	public Image perkIcon;
	public Button[] equipButtons;

	public bool chosen;
	private int thisIndex = -1;

	public void init()
	{
		if (perkName == "") // perkName not set.  Don't do anything.
			return;
		
		perk = (Passive)Activator.CreateInstance (Type.GetType ("Passives." + perkName));

		thisIndex = PlayerPerks.isDuplicate (perk, PlayerPerks.passives);
		chosen = (thisIndex != -1);

		perkDescription.text = "<i>" + perk.Name + "</i>\n" + perk.Desc;
		perkIcon.sprite = perk.Icon;
	}

	// Set the ith index of the player's passives to perk
	public void setPerk(int i)
	{
		int dupPos = PlayerPerks.isDuplicate (perk, PlayerPerks.passives);
		if (dupPos != -1)
		{
			summary.swapPassives (dupPos, i);
			thisIndex = i;
			return;
		}

		summary.changePassive (i, perk);

		chosen = true;
		thisIndex = i;
		thisSlot += new ChangedSlotPassive(removePerk);
	}

	// Called when the ChangedSlotPassive event is invoked
	private void removePerk(int i)
	{
		if (i != thisIndex)
			return;
		
		chosen = false;
		thisIndex = -1;
		thisSlot -= new ChangedSlotPassive(removePerk);
	}
}
