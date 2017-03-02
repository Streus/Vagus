using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
	[SerializeField]
	public int captureValue{ get; private set; }
	[SerializeField]
	private int captureSpeed;
	private int dValue;
	private float delayTimer;
	public int captureValueMax = 100;
	[HideInInspector]
	public Faction capturingTeam;

	private List<Entity> competitors;

	private CaptureNode thisNode;

	public void Awake()
	{
		captureValue = 0;
		capturingTeam = Faction.neutral;

		delayTimer = 0f;

		competitors = new List<Entity> ();

		thisNode = transform.parent.GetComponent<CaptureNode> ();
	}

	public void Update()
	{
		delayTimer += Time.deltaTime;
		if (delayTimer > 1f)
		{
			delayTimer = 0f;

			captureValue += dValue;
			if (captureValue >= captureValueMax)
				thisNode.captured (capturingTeam);
			else if (captureValue <= 0)
			{
				dValue = captureValue = 0;
				capturingTeam = Faction.neutral;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		Entity ent = col.GetComponent<Entity> ();
		if (ent == null)
			return;

		competitors.Add (ent);

		// Update UI if the Entity is local
		Controller con = ent.GetComponent<Controller> ();
		if (con.isLocalPlayer)
			HUDControl.hud.CurrentNode = thisNode;

		if (capturingTeam == Faction.neutral)
		{
			capturingTeam = ent.faction;
			dValue = captureSpeed;
		}
		else if (capturingTeam != ent.faction)
		{
			if (areCompetitorsOfFaction (capturingTeam))
				dValue = 0;
			else
				dValue = -captureSpeed;
		}
		else if (capturingTeam == ent.faction)
		{
			int wt = winningTeam ();
			if (wt == -1)
				dValue = 0;
			else
				dValue = captureSpeed;
		}
	}

	public void OnTriggerStay2D(Collider2D col)
	{
		Entity ent = col.GetComponent<Entity> ();
		if (ent == null)
			return;


	}

	public void OnTriggerExit2D(Collider2D col)
	{
		Entity ent = col.GetComponent<Entity> ();
		if (ent == null)
			return;

		competitors.Remove (ent);

		// Update UI if the Entity is local
		Controller con = ent.GetComponent<Controller> ();
		if (con.isLocalPlayer)
			HUDControl.hud.CurrentNode = null;

		int winTeam = winningTeam ();
		if (winTeam == -1)
			dValue = 0;
		else if (winTeam == -2 || (Faction)winTeam != capturingTeam)
			dValue = -captureSpeed;
		
	}

	private bool areCompetitorsOfFaction(Faction faction)
	{
		foreach (Entity e in competitors)
			if (e.faction == faction)
				return true;
		return false;
	}

	private int winningTeam()
	{
		int wt = -2; //nobody is on the node
		foreach (Entity e in competitors)
		{
			if (wt == -2)
				wt = (int)(e.faction);
			else if (wt != (int)(e.faction))
				return -1; //more than one faction is on the node
		}
		return wt;
	}
}
