using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
	public static LobbyMenu singleton;
	public GameObject playerSummaryPrefab;

	public Transform playerList;

	public void Start()
	{
		if(singleton == null)
			singleton = this;
		else
			throw new UnityException("More than one LobbyMenu!");
	}

	public GameObject addPlayerSummary(GameObject player, string name)
	{
		GameObject playerSum = (GameObject)Instantiate(playerSummaryPrefab);
		playerSum.transform.SetParent(transform.GetChild(0).GetChild(1), false);
		playerSum.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name;
		playerSum.transform.GetChild(1).GetComponent<Image>().color = Color.white;
		NetworkServer.SpawnWithClientAuthority(playerSum, player);

		return playerSum;
	}

	public void removePlayerSummary(GameObject player, GameObject summary)
	{
		if(player.GetComponent<NetworkLobbyPlayer>().hasAuthority)
		{
			NetworkServer.UnSpawn(summary);
		}
	}
}
