using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

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
	public void Awake()
	{
		//setup singleton
		lobbyManager = this;
		DontDestroyOnLoad (gameObject);

		//load in player perk selections
		PlayerPerks.loadPassives();
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
		base.OnLobbyServerSceneLoadedForPlayer (lobbyPlayer, gamePlayer);

		CustomLobbyPlayer lp = lobbyPlayer.GetComponent<CustomLobbyPlayer> ();
		Entity playerStats = gamePlayer.GetComponent<Entity> ();

		//set gameobject's name to player's chosen name
		gamePlayer.name = lp.playerName;

		//set the player's faction depending on team
		playerStats.faction = (Faction)(lp.playerTeam + 1);

		//set the player's passives
		for(int i = 0; i < playerStats.equipment.Length; i++)
		{
			playerStats.addPassive((Passive)Activator.CreateInstance (Type.GetType (lp.passives [i]), playerStats), i);
		}

		return true;
	}


}

// Holds the Passives to be applied to the local player on game start
public static class PlayerPerks
{
	public static Passive[] passives;

	// Loads and instantiates passives
	public static void loadPassives()
	{
		passives = new Passive[4];
		for (int i = 0; i < passives.Length; i++)
		{
			string pass = PlayerPrefs.GetString ("passive" + i, "");
			if(pass != "")
				passives[i] = (Passive)Activator.CreateInstance(Type.GetType(pass));
		}
	}

	// Saves each passive in passives as a string in PlayerPrefs
	public static void savePassives()
	{
		for (int i = 0; i < passives.Length; i++)
		{
			string pass = passives [i].GetType ().AssemblyQualifiedName;
			PlayerPrefs.SetString ("passive" + i, pass);
		}
	}

	// Helper method that checks for duplicates in a Passive array
	// returns the index of the duplicate, -1 if there is none
	public static int isDuplicate(Passive passive, Passive[] passiveList)
	{
		for (int i = 0; i < passiveList.Length; i++)
			if (passiveList [i] != null && passiveList[i].Equals (passive))
				return i;
		return -1;
	}

	// Helper method that swaps the values of two positions in passives
	public static void swapPassives(int position1, int position2)
	{
		if (!(position1 < passives.Length && position2 < passives.Length))
			return;

		Passive temp = passives [position1];
		passives [position1] = passives [position2];
		passives [position2] = temp;
	}

	// Helper method that converts an array of Passives into an array of their AssemblyQualifiedNames
	public static string[] toStringArray(Passive[] passiveList)
	{
		string[] aqns = new string[passiveList.Length];
		for (int i = 0; i < passiveList.Length; i++)
		{
			if (passiveList [i] != null)
				aqns [i] = passiveList [i].GetType ().AssemblyQualifiedName;
			else
				aqns [i] = "";
		}
		return aqns;
	}
}