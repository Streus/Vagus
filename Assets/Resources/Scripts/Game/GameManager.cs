using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
	public static GameManager manager;

	public static readonly string[] nodeNames = 
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
	public float matchTime; //ONLY TO BE SYNCED ON PREGAME START

	[HideInInspector]
	public GameObject[] nodes;

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
		//nodes = new GameObject[1];
		//GameObject nodePrefab = Resources.Load<GameObject>("Prefabs/Game/Misc/Node");
		//for (int i = 0; i < nodes.Length; i++)
		//{
		//	nodes [i] = (GameObject)Instantiate (nodePrefab, Vector3.zero, Quaternion.identity);
		//	CaptureNode cn = nodes [i].GetComponent<CaptureNode> ();
		//	cn.team = 0;
		//	NetworkServer.Spawn (nodes [i]);
		//}

		//setup initial game state
		CmdChangeGameState ((int)GameState.pregame);
	}

	public void Update()
	{
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
			CmdChangeMatchTime (0f);
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

	// Match Time mutators
	public void OnMatchTime(float time)
	{
		if(time > matchTime)
			matchTime = time;
	}
	[Command]
	public void CmdChangeMatchTime(float time)
	{
		matchTime = time;
	}
}

public enum GameState
{
	pregame,
	round,
	postgame
}
