using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HostSetupFunctions : MonoBehaviour
{
	public InputField IPInputField;
	public InputField IPDisplayField;
	public InputField portInputField;
	public InputField passwordInputField;
	public Menu lobbyMenu;

	public void Start()
	{
		if(IPDisplayField != null)
			IPDisplayField.text = NetworkLobbyManager.singleton.networkAddress;
		portInputField.text = "7777";
		NetworkLobbyManager.singleton.networkPort = 7777;

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

	public void hostServer()
	{
		NetworkLobbyManager.singleton.StartHost();
		MenuManager.menusys.showMenu(lobbyMenu);
	}
}
