using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyMenu : NetworkBehaviour
{
	public static LobbyMenu singleton;

	public GameObject playerSummaryPrefab;

	public CustomLobbyPlayer localPlayer;

	public List<CustomLobbyPlayer> players = new List<CustomLobbyPlayer> ();
	public Transform playerListTransform;

	public void Start()
	{
		if(singleton == null)
			singleton = this;
		else
			throw new UnityException("More than one LobbyMenu!");
		
		localPlayer = null;
	}

	public PlayerSummary addPlayerSummary(CustomLobbyPlayer player)
	{
		if (players.Contains (player))
			return null;

		players.Add (player);

		GameObject playerSum = (GameObject)Instantiate(playerSummaryPrefab);
		PlayerSummary summary = playerSum.GetComponent<PlayerSummary> ();
		playerSum.transform.SetParent(playerListTransform, false);
		summary.nameField.text = player.playerName;
		summary.readyIndicator.color = Color.white;

		return summary;
	}

	public void removePlayerSummary(CustomLobbyPlayer player)
	{
		if (!players.Contains (player))
			return;

		players.Remove (player);

		Destroy (player.summary.gameObject);
	}

	public void OnReady()
	{
		localPlayer.SendReadyToBeginMessage ();
	}

	public void OnChangeName(string name)
	{
		CmdChangeName (name);
	}

	[Command]
	public void CmdChangeName(string name)
	{
		localPlayer.playerName = name;
	}
}
