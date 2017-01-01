using UnityEngine;
using System.Collections;
using System;

public class Room : IComparable
{
	/* Instance vars */
	public bool leftDoor;
	public bool forDoor;
	public bool rightDoor;
	public bool backDoor;
	public bool upDoor;
	public bool downDoor;

	public int roomIndex;

	/* Constructor */
	public Room()
	{

	}
}
