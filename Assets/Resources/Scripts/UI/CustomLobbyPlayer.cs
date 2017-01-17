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

		LobbyMenu.singleton.localPlayer = this;
	}

	public override void OnClientEnterLobby ()
	{
		base.OnClientEnterLobby ();
		summary = LobbyMenu.singleton.addPlayerSummary(gameObject, "Player " + (slot + 1));
	}

	public override void OnClientExitLobby ()
	{
		base.OnClientExitLobby ();
		LobbyMenu.singleton.removePlayerSummary(gameObject, summary);
	}

	public override void OnClientReady (bool readyState)
	{
		base.OnClientReady (readyState);
		if(readyState)
			readyIndicator.color = new Color(0f, 1f, 1f);
		else
			readyIndicator.color = Color.white;
	}
}
