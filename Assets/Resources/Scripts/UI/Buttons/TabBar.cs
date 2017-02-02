using UnityEngine;
using System.Collections;

public class TabBar : MonoBehaviour
{
	public bool activateFirst;
	public GameObject[] tabs;
	private int currentTab;

	public void Start()
	{
		//hide all tabs expect for the first one
		foreach(GameObject tab in tabs)
		{
			tab.SetActive (false);
		}
		if(tabs.Length > 0)
			tabs[0].SetActive(activateFirst);
		currentTab = 0;
	}

	// Invoked by buttons in the TabBar
	public void goToTab(int tabNumber)
	{
		if (tabNumber >= tabs.Length)
			throw new UnityException ("TabBar out of bounds!");

		tabs [currentTab].SetActive (false);
		tabs [tabNumber].SetActive (true);
		currentTab = tabNumber;
	}

	public void toggleTab(int tabNumber)
	{
		if (tabNumber >= tabs.Length)
			throw new UnityException ("TabBar out of bounds!");

		tabs [tabNumber].SetActive (!tabs [tabNumber].activeSelf);
	}
}
