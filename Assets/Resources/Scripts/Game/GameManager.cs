using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
	public static GameManager manager;

	private static string[] nodeNames = 
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

	[SyncVar(hook = "OnMatchTime")]
	public float matchTime;

	[HideInInspector]
	public CaptureNode[] nodes;

	public bool canEditNodes;

	public bool inPauseMenu;
	public Menu pauseMenu;
	public Menu postgameMenu;

	/* Instance Methods */
	public void Awake()
	{
		//setup singleton
		if(manager == null)
			manager = this;

		inPauseMenu = false;

		//spawn nodes TODO only test version
		if(isServer)
		{
			nodes = new CaptureNode[3];
			shuffleNodeNames (5);
			GameObject nodePrefab = Resources.Load<GameObject>("Prefabs/Game/Misc/Node");
			float nodeX = 0f;
			float nodeY = 100f;
			for (int i = 0; i < nodes.Length; i++)
			{
				GameObject node = (GameObject)Instantiate (nodePrefab, new Vector3(nodeX, nodeY + 50f * i, 0f), Quaternion.identity);
				nodes [i] = node.GetComponent<CaptureNode> ();
				nodes [i].team = 0;
				nodes [i].name = nodeNames [i];
				NetworkServer.Spawn (nodes [i].gameObject);
			}

			//setup initial game state
			CmdChangeGameState ((int)GameState.pregame);
		}
	}

	// Randomly swaps the elements of nodeNames
	private void shuffleNodeNames(int permutations)
	{
		for (int i = 0; i < permutations; i++)
		{
			for (int j = 0; j < nodeNames.Length; j++)
			{
				int randPos = (int)(Random.value * (nodeNames.Length - 1));
				string temp = nodeNames [j];
				nodeNames [j] = nodeNames [randPos];
				nodeNames [randPos] = temp;
			}
		}
	}

	public void Update()
	{
		if(isServer)
			matchTime += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Escape) && 
			((GameState)gameState == GameState.round || (GameState)gameState == GameState.pregame))
			togglePause ();

		switch ((GameState)gameState)
		{
		case GameState.pregame:
			
			break;
		case GameState.round:
			
			break;
		case GameState.postgame:
			
			break;
		}
	}

	public void togglePause()
	{
		inPauseMenu = !inPauseMenu;
		if (inPauseMenu)
		{
			MenuManager.menusys.showMenu (pauseMenu);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			MenuManager.menusys.returnToPreviousMenu ();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	// GameState mutators
	public void OnGameState(int state)
	{
		gameState = state;
		switch ((GameState)state)
		{
		case GameState.pregame:
			//begin a countdown to first round start, then move to pause
			if (isServer)
				matchTime = 0f;
			togglePause ();
			break;
		case GameState.round:
			//default behavior.  move into pause when asked.  move into postgame when win condition is met

			break;
		case GameState.postgame:
			//display end-of-game stats, allow for replay
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			break;
		}
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
	[Command]
	public void CmdChangeRound(int round)
	{
		this.round = round;
	}

	// MatchTime Mutators
	public void OnMatchTime(float time)
	{
		this.matchTime = time;
	}
}

public enum GameState
{
	pregame,
	round,
	postgame
}
