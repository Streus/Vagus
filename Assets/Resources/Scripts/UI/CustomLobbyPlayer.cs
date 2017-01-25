using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomLobbyPlayer : NetworkLobbyPlayer
{
	[SyncVar(hook = "OnThisName")]
	public string playerName;

	public PlayerSummary summary;

	public override void OnClientEnterLobby ()
	{
		base.OnClientEnterLobby ();
		CustomLobbyManager.lobbyManager.NumClientPlayers++;

		summary = LobbyMenu.singleton.addPlayerSummary (this);

		if (isLocalPlayer)
			SetupLocal ();
		else
			SetupRemote ();

		OnThisName (playerName);
	}

	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();

		SetupLocal ();
	}

	public void SetupLocal()
	{
		OnClientReady (false);
		LobbyMenu.singleton.localPlayer = this;
	}

	public void SetupRemote()
	{
		OnClientReady (false);
	}

	public void OnThisName(string name)
	{
		playerName = name;
		summary.nameField.text = name;
	}

	public override void OnClientReady (bool readyState)
	{
		if (readyState)
		{
			summary.readyIndicator.color = Color.cyan;
		}
		else
		{
			summary.readyIndicator.color = Color.white;
		}
	}

	public void OnDestroy()
	{
		LobbyMenu.singleton.removePlayerSummary (this);
		CustomLobbyManager.lobbyManager.NumClientPlayers--;
	}
}
