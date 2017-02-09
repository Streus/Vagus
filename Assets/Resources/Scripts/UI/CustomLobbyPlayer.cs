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

	public SyncListString passives = new SyncListString ();

	public PlayerSummary summary;

	public override void OnClientEnterLobby ()
	{
		base.OnClientEnterLobby ();
		CustomLobbyManager.lobbyManager.NumClientPlayers++;

		summary = LobbyMenu.singleton.addPlayerSummary (this);
		passives.Callback = OnPassivesChanged;

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

		CmdChangeName(PlayerPrefs.GetString ("playername", ""));
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

			string[] passNames = PlayerPerks.toStringArray (PlayerPerks.passives);
			for (int i = 0; i < passNames.Length; i++)
			{
				CmdSetPassive (i, passNames [i]);
			}
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
		PlayerPrefs.SetString ("playername", playerName);
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
			summary.teamIndicator.color = new Color(1f, 0.5f, 0f, 1f);
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

	// Passives mutators
	public void setPassive(int index, string passive)
	{
		CmdSetPassive (index, passive);
	}
	[Command]
	public void CmdSetPassive(int index, string passive)
	{
		passives.Insert(index, passive);
	}
	public void OnPassivesChanged(SyncListString.Operation op, int index)
	{
		
	}

	public void OnDestroy()
	{
		LobbyMenu.singleton.removePlayerSummary (this);
		CustomLobbyManager.lobbyManager.NumClientPlayers--;
	}
}
