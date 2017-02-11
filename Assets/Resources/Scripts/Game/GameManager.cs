using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
	public static GameManager manager;

	public static string[] nodeNames = 
	{
		"abc", "akd", "ake", "afh",
		"bgi", "bgc", "bel", "bed",
		"cag", "cgf", "cjh", "cil",
		"dbh", "dlb", "dka", "dgk",
		"efg", "eak", "ehj", "ecg",
		"fcb", "fck", "fcd", "fce",
		"gjc", "gkh", "gcl", "gcj",
		"hcd", "hce", "hci", "hkj",
		"iaf", "ihk", "ild", "ieg",
		"jcf", "jkh", "jhe", "jla",
		"kfg", "kfe", "kgd", "kae",
		"lic", "leb", "lcg", "lcj"
	};

	/* Instance Vars */
	[SyncVar(hook = "OnGameState")]
	public int gameState;

	[SyncVar(hook = "OnTeamAScore")]
	public int teamAScore;

	[SyncVar(hook = "OnTeamBScore")]
	public int teamBScore;

	[SyncVar(hook = "OnNextNodeName")]
	public string nextNodeName;

	[SyncVar(hook = "OnRound")]
	public int round;

	/* Instance Methods */
	public void Awake()
	{
		if(manager == null)
			manager = this;


	}

	public void Update()
	{
		
	}

	public float getMatchTime()
	{
		return Time.timeSinceLevelLoad;
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

	// TeamAScore mutators
	public void OnTeamAScore(int score)
	{
		teamAScore = score;
	}
	public void changeTeamAScore(int score)
	{
		CmdChangeTeamAScore (score);
	}
	[Command]
	public void CmdChangeTeamAScore(int score)
	{
		teamAScore = score;
	}

	// TeamBScore mutators
	public void OnTeamBScore(int score)
	{
		teamBScore = score;
	}
	public void changeTeamBScore(int score)
	{
		CmdChangeTeamBScore (score);
	}
	[Command]
	public void CmdChangeTeamBScore(int score)
	{
		teamBScore = score;
	}

	// NextNodeName mutators
	public void OnNextNodeName(string name)
	{
		nextNodeName = name;
	}
	public void changeNextNodeName(string name)
	{
		CmdChangeNextNodeName (name);
	}
	[Command]
	public void CmdChangeNextNodeName(string name)
	{
		nextNodeName = name;
	}

	// Round mutators
	public void OnRound(int round)
	{
		this.round = round;
	}
	public void changeRound(int round)
	{
		CmdChangeRound (round);
	}
	[Command]
	public void CmdChangeRound(int round)
	{
		this.round = round;
	}
}

public enum GameState
{
	pregame,
	round,
	postgame,
	pause
}
