using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
	public static GameManager manager;

	/* Instance Vars */
	[SyncVar(hook = "OnGameState")]
	public int gameState;

	/* Instance Methods */
	public void Awake()
	{
		if(manager == null)
			manager = this;
	}

	public void Update()
	{

	}

	// GameState mutators
	public void OnGameState(int state)
	{
		gameState = state;
	}
	public void changeGameState(int state)
	{
		CmdChangeGameState(state);
	}
	[Command]
	public void CmdChangeGameState(int state)
	{
		gameState = state;
	}
}

public enum GameState
{
	pregame,
	round,
	postgame,
	pause
}
