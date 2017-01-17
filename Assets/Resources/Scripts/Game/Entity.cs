using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Delegate for when an entity takes damage from a bullet
public delegate void EntityDamaged();

//Delegate for when the status list is updated
public delegate void UpdatedStatusList(Status newStatus);

public class Entity : MonoBehaviour
{
	/* Instance Vars */

	private float shield;
	public float shieldMax;
	public float shieldRegen;
	public float shieldRegenDelay;

	private float health;
	public float healthMax;

	public int speed;

	public Faction faction;

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
			if (health <= 0f)
				die ();
		}
	}
	public float Shield
	{
		get{ return shield; }
		set
		{
			shield = value;
			if(shield > shieldMax)
				shield = shieldMax;
			if(shield <= 0f)
			{
				shield = 0f;
				shieldMax = 0f;
				shieldRegen = 0f;
			}
		}
	}
	public float ShieldMax
	{
		get{ return shieldMax; }
		set{ shieldMax = value; }
	}
	public float ShieldRegen
	{
		get{ return shieldRegen; }
		set{ shieldRegen = value; }
	}
	public float HealthMax
	{
		get{ return healthMax; }
		set{ healthMax = value; }
	}
	public int Speed
	{
		get{ return speed; }
		set{ speed = value; }
	}

	/* Instance Methods */

	// Setup internal variables
	public void Awake()
	{
		health = healthMax;
		equipment = new Passive[5];
		statusEffects = new List<Status> ();

		animator = transform.GetComponent<Animator> ();
		controller = transform.GetComponent<Controller> ();
	}

	public void Update()
	{
		//TODO entity update
	}

	// Play a death animation and destroy this gameobject
	public void die()
	{
		
	}

	/* Events */

	// Entity takes damage
	public event EntityDamaged entityDam;
	public virtual void OnEntityDamaged()
	{
		if(entityDam != null)
			entityDam();
	}

	// StatusList is updated
	public event UpdatedStatusList updateStatus;
	public virtual void OnStatusListUpdated(Status newStatus)
	{
		if(updateStatus != null)
			updateStatus(newStatus);
	}
}