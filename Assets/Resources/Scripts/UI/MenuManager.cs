using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public static MenuManager menusys;

	public Menu currentMenu;
	private Menu[] menus;

	public void Start()
	{
		if(menusys == null)
			menusys = this;
		else
			throw new UnityException("More then one MenuManager in scene!");

		menus = FindObjectsOfType<Menu>();

		showMenu(currentMenu);
	}

	// Retrieve a specific menu for the menu list
	public Menu getMenu(string name)
	{
		foreach(Menu menu in menus)
			if(menu.gameObject.name == name)
				return menu;
		return null;
	}

	// Switch to menu from the current menu
	public void showMenu(Menu menu)
	{
		if(currentMenu != null)
			currentMenu.IsOpen = false;
		currentMenu = menu;
		currentMenu.IsOpen = true;
	}
}
