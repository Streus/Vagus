using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchSetupFunctions : MonoBehaviour
{
	public InputField IPInputField;
	public InputField portInputField;
	public InputField passwordInputField;
	public Menu lobbyMenu;

	public void Start()
	{
		IPInputField.text = PlayerPrefs.GetString ("defaultip", "127.0.0.1");
		updateServerIP ();
		portInputField.text = "" + PlayerPrefs.GetInt ("defaultport", 25000);
		updateServerPort ();
	}

	public void updateServerIP()
	{
		CustomLobbyManager.lobbyManager.networkAddress = IPInputField.text;
		PlayerPrefs.SetString ("defaultip", CustomLobbyManager.lobbyManager.networkAddress);
	}

	public void updateServerPort()
	{
		CustomLobbyManager.lobbyManager.networkPort = int.Parse(portInputField.text);
		PlayerPrefs.SetInt ("defaultip", CustomLobbyManager.lobbyManager.networkPort);
	}

	public void updateServerPassword()
	{
		Network.incomingPassword = passwordInputField.text;
	}

	public void connectToServer()
	{
		CustomLobbyManager.lobbyManager.StartClient();
		MenuManager.menusys.showMenu(lobbyMenu);
	}

	public void hostServer()
	{
		CustomLobbyManager.lobbyManager.StartHost();
		MenuManager.menusys.showMenu(lobbyMenu);
	}
}
