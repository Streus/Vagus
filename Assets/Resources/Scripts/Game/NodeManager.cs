using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// A server only utility class that spawns nodes and selects the next
// "read" node.
public class NodeManager : NetworkBehaviour
{
	/* Static Vars */
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
	private CaptureNode[] nodes;
	private CaptureNode.NodeCaptured capEvents;

	/* Instance Methods */
	public void OnStartServer()
	{
		//spawn nodes TODO only test version
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
			nodes [i].captureEvent += new CaptureNode.NodeCaptured (OnNodeCaptured);
			NetworkServer.Spawn (nodes [i].gameObject);
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

	public void OnNodeCaptured(CaptureNode cn, Faction capTeam)
	{

	}
}
