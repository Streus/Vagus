﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CaptureNode : MonoBehaviour
{
	public int team;

	private CaptureArea captureZone;

	public void Start()
	{
		captureZone = transform.GetChild (0).GetComponent<CaptureArea> ();
	}

	public float getCapRatio()
	{
		return ((float)captureZone.captureValue) / ((float)captureZone.captureValueMax);
	}

	public Faction getCapTeam()
	{
		return captureZone.capturingTeam;
	}

	public delegate void NodeCaptured(CaptureNode self, Faction capturingTeam);
	public event NodeCaptured captureEvent;
	public void captured(Faction capturingTeam)
	{
		Debug.Log ("Node has been captured by: " + capturingTeam.ToString ());
		if (captureEvent != null)
			captureEvent (this, capturingTeam);
	}
}
