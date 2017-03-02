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
		//TODO this broke
		if (Network.isServer)
			CustomLobbyManager.Shutdown ();
		CustomLobbyManager.lobbyManager.StopClient ();
	}
}