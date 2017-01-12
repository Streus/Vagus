using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ExitButton : MonoBehaviour
{
	public void exitGame()
	{
		Application.Quit();
	}

	public void exitMatch()
	{
		NetworkLobbyManager.singleton.StopClient();
	}
}