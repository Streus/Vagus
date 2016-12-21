using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
	/* Essentials */
	private float health;
	public float healthMax;
	public float healthRegen;

	public int speed;

	private Passive[] equipment;
	private List<Status> statusEffects;
	private Ability primaryFire;
	private Ability secondaryAbility;

	private Animator animator;
	private Controller controller;

	/* Accessors */
	public float Health
	{
		get{ return health; }
		set
		{
			health = value;
			if (health > healthMax)
				health = healthMax;
			if (health <= 0)
				die ();
		}
	}
	public float HealthMax
	{
		get{ return healthMax; }
		set{ healthMax = value; }
	}
	public float HealthRegen
	{
		get{ return healthRegen; }
		set { healthRegen = value; }
	}
	public int Speed
	{
		get{ return speed; }
		set{ speed = value; }
	}

	/* Methods */
	// Setup internal variables
	public void Awake()
	{
		health = healthMax;
		equipment = new Passive[5];
		statusEffects = new List<Status> ();

		animator = transform.GetComponent<Animator> ();
		controller = transform.GetComponent<Controller> ();
	}

	// Play a death animation and destroy this gameobject
	public void die()
	{
		
	}
}