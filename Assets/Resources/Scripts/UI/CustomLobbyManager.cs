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

	/* Instance Methods */
	public void Start()
	{
		//setup singleton
		lobbyManager = this;
		DontDestroyOnLoad (gameObject);

		//load in player perk selections
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
		CustomLobbyPlayer lp = lobbyPlayer.GetComponent<CustomLobbyPlayer> ();
		Entity playerStats = gamePlayer.GetComponent<Entity> ();

		//set gameobject's name to player's chosen name
		gamePlayer.name = lp.playerName;

		//set the player's faction depending on team
		playerStats.faction = (Faction)(lp.playerTeam + 1);

		//set the player's passives
		//TODO

		return true;
	}


}

// Holds the Passives to be applied to the local player on game start as strings of the 
// Passive class names.
public static class PlayerPerks
{
	public static string passive1;
	public static string passive2;
	public static string passive3;
	public static string passive4;
}