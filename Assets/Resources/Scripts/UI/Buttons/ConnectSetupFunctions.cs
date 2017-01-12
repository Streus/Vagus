using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectSetupFunctions : MonoBehaviour
{
	public InputField IPInputField;
	public InputField portInputField;
	public InputField passwordInputField;
	public Menu lobbyMenu;

	public void Start()
	{
		//TODO maybe include support for saving default address & port
	}

	public void updateServerIP()
	{
		NetworkLobbyManager.singleton.networkAddress = IPInputField.text;
	}

	public void updateServerPort()
	{
		NetworkLobbyManager.singleton.networkPort = int.Parse(portInputField.text);
	}

	public void updateServerPassword()
	{
		Network.incomingPassword = passwordInputField.text;
	}

	public void connectToServer()
	{
		NetworkLobbyManager.singleton.StartClient();
		MenuManager.menusys.showMenu(lobbyMenu);
	}
}
