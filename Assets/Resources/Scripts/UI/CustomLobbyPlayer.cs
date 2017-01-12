using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomLobbyPlayer : NetworkLobbyPlayer
{
	public Image readyIndicator;
	private GameObject summary;

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		readyToBegin = false;
		SendNotReadyToBeginMessage();
	}

	public override void OnClientEnterLobby ()
	{
		base.OnClientEnterLobby ();
		summary = LobbyMenu.singleton.addPlayerSummary(gameObject, "Player " + NetworkLobbyManager.singleton.numPlayers);
	}

	public override void OnClientExitLobby ()
	{
		base.OnClientExitLobby ();
		LobbyMenu.singleton.removePlayerSummary(gameObject, summary);
	}

	public override void OnClientReady (bool readyState)
	{
		base.OnClientReady (readyState);
	}
}
