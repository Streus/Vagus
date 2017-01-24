using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager
{
	public static CustomLobbyManager lobbyManager;

	/* Instance Vars */
	private int numClientPlayers;

	/* Accessors */
	public int NumClientPlayers
	{
		get{ return numClientPlayers; }
		set{ numClientPlayers = value; }
	}

	public void Start()
	{
		lobbyManager = this;
		DontDestroyOnLoad (gameObject);
	}

	public void AddLocalPlayer()
	{
		TryToAddPlayer ();
	}

	public void RemovePlayer(CustomLobbyPlayer player)
	{
		player.RemovePlayer ();
	}

	// Play scene has finished loading.  Apply custom values from lobbyPlayer to gamePlayer
	public override bool OnLobbyServerSceneLoadedForPlayer (GameObject lobbyPlayer, GameObject gamePlayer)
	{
		return base.OnLobbyServerSceneLoadedForPlayer (lobbyPlayer, gamePlayer);
	}
}
