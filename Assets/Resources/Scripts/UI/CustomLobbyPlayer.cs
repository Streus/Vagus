using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomLobbyPlayer : NetworkLobbyPlayer
{
	[SyncVar(hook = "OnThisName")]
	public string playerName;

	[SyncVar(hook = "OnThisTeam")]
	public int playerTeam;

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
		OnThisTeam (playerTeam);
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

	public override void OnClientReady (bool readyState)
	{
		if (summary == null)
		{
			Debug.LogError ("Summary not yet initialized!");
			return;
		}

		if (readyState)
		{
			summary.readyIndicator.color = Color.cyan;
		}
		else
		{
			summary.readyIndicator.color = Color.white;
		}
	}

	// Name field mutators
	public void OnThisName(string name)
	{
		playerName = name;
		summary.nameField.text = name;
	}
	public void changeName(string name)
	{
		CmdChangeName (name);
	}
	[Command]
	public void CmdChangeName(string name)
	{
		playerName = name;
	}

	// Team field mutators
	public void OnThisTeam(int team)
	{
		playerTeam = team;
		switch (team)
		{
		case 0:
			summary.teamIndicator.color = Color.red;
			break;
		case 1:
			summary.teamIndicator.color = Color.blue;
			break;
		}
	}
	public void changeTeam(int team)
	{
		CmdChangeTeam (team);
	}
	[Command]
	public void CmdChangeTeam(int team)
	{
		playerTeam = team;
	}

	public void OnDestroy()
	{
		LobbyMenu.singleton.removePlayerSummary (this);
		CustomLobbyManager.lobbyManager.NumClientPlayers--;
	}
}
