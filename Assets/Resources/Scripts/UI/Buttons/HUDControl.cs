using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{
	public static HUDControl hud;

	/* Instance Vars */
	[SerializeField]
	private Text roundNumber;
	[SerializeField]
	private Text nextNodeDisplay;
	[SerializeField]
	private Text score;
	[SerializeField]
	private Text matchTime;

	private CaptureNode currentNode;
	[SerializeField]
	private GameObject currentNodeSummary;
	[SerializeField]
	private Image currentNodeProgress;
	[SerializeField]
	private Text currentNodeName;

	public Entity subject;
	[SerializeField]
	private AbilitySlot[] abilitySlots;
	[SerializeField]
	private GameObject statusList;
	[SerializeField]
	private Image healthBar;
	[SerializeField]
	private Image shieldsBar;

	/* Accessors */
	public CaptureNode CurrentNode
	{
		get { return currentNode; }
		set
		{
			currentNode = value;
			currentNodeSummary.SetActive (currentNode != null);
			if(currentNodeSummary.activeSelf)
				currentNodeName.text = currentNode.name;
		}
	}

	/* Instance Methods */
	public void Awake()
	{
		if (hud == null)
			hud = this;
		else
			throw new UnityException ("More than one HUD!");

		CurrentNode = null;
	}

	public void Update()
	{
		// Top Panel
		roundNumber.text = "Round\n" + GameManager.manager.round + "/24";

		//TODO implement slow reveal of nextNodeName value
		nextNodeDisplay.text = GameManager.manager.nextNodeName;

		int s1 = GameManager.manager.teamAScore;
		int s2 = GameManager.manager.teamBScore;
		score.text = "<color=#ff7700ff>" + s1 + "</color>:<color=#0000ffff>" + s2 + "</color>";

		int mt =  Mathf.RoundToInt (GameManager.manager.matchTime);
		int matchMinutes = mt / 60;
		int matchSeconds = mt % 60;
		matchTime.text = matchMinutes.ToString("00") + ":" + matchSeconds.ToString("00");

		// Current Node Display
		if (currentNodeSummary.activeSelf)
		{
			currentNodeProgress.fillAmount = currentNode.getCapRatio ();
			currentNodeProgress.color = Bullet.factionColor (currentNode.getCapTeam ());
		}
			

		// Bottom Panel

	}
}
