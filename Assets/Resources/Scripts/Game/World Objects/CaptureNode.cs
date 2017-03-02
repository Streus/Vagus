﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaptureNode : MonoBehaviour
{
	public Faction team;

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

	public void captured(Faction capturingTeam)
	{
		Debug.Log ("Node has been captured by: " + capturingTeam.ToString ());
	}
}
