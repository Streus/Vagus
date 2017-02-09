using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{
	public static HUDControl hud;

	public Text roundNumber;
	public Text nextNodeDisplay;
	public Text score;
	public Text matchTime;

	public Entity subject;
	public AbilitySlot[] abilitySlots;
	public GameObject statusList;
	public Image healthBar;
	public Image shieldsBar;

	public void Awake()
	{
		if (hud == null)
			hud = this;
		else
			throw new UnityException ("More than one HUD!");
	}

	public void Update()
	{
		roundNumber.text = "Round " + GameManager.manager.round;

		//TODO implement slow reveal of nextNodeName value
		nextNodeDisplay.text = GameManager.manager.nextNodeName;

		int s1 = GameManager.manager.teamAScore;
		int s2 = GameManager.manager.teamBScore;
		score.text = "<color=#ff7700ff>" + s1 + "</color>:<color=#0000ffff>" + s2 + "</color>";

		int mt =  Mathf.RoundToInt (GameManager.manager.getMatchTime ());
		int matchMinutes = mt / 60;
		int matchSeconds = mt % 60;
		matchTime.text = matchMinutes.ToString("00") + ":" + matchSeconds.ToString("00");

	}
}
