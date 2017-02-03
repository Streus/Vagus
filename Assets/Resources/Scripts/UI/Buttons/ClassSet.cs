using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClassSet : MonoBehaviour
{
	private PerkDescription[] perks;

	public void Start()
	{
		perks = new PerkDescription[4];
		for (int i = 0; i < perks.Length; i++)
		{
			perks [i] = transform.GetChild (1).GetChild (i).GetComponent<PerkDescription> ();
			perks [i].init ();
		}
	}

	public void Update()
	{
		foreach (Button b in perks[perks.Length - 1].equipButtons)
			b.interactable = perks [0].chosen;
	}
}
