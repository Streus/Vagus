using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Controller))]
public class Entity : MonoBehaviour
{
	/* Instance Vars */

	// A regenerative hit point pool that takes damage before health
	private float shield;
	public float shieldMax = 50f;
	public float shieldMaxPerc = 1f;

	public float shieldRegen = 1f;
	public float srDelayMax;
	private float srDelayCurr;
	public float srDelayPerc = 1f;

	// The primary hit point pool.  Entity dies when it reaches 0.
	private float health;
	public float healthMax = 50f;
	public float healthMaxPerc = 1f;

	// A scalar applied to movement vectors
	public int speed = 1;

	// Used to determine teams
	public Faction faction;

	[HideInInspector]
	public Passive[] equipment;
	[HideInInspector]
	public Ability[] abilities;
	private List<Status> statusEffects;

	private Animator animator;
	private Controller controller;

	/* Accessors */
	public float currentHealth
	{
		get{ return health; }
		set
		{
			health = value;
			if (health > effectiveHealthMax)
				health = effectiveHealthMax;
			if (health <= 0f)
				die ();
		}
	}
	public float effectiveHealthMax
	{
		get{ return healthMax * healthMaxPerc; }
	}

	public float currentShield
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
				resetSRDelay ();
			}
		}
	}
	public float effectiveShieldMax
	{
		get{ return shieldMax * shieldMaxPerc; }
	}

	public float effectiveSRDelay
	{
		get{ return srDelayMax * srDelayPerc; }
	}

	public void resetSRDelay()
	{
		srDelayCurr = effectiveSRDelay;
	}

	public void getStatusList(out List<Status> statuses)
	{
		statuses = this.statusEffects;
	}

	/* Instance Methods */

	// Setup internal variables
	public void Awake()
	{
		health = effectiveHealthMax;
		shield = effectiveShieldMax;
		equipment = new Passive[4];
		abilities = new Ability[4];
		statusEffects = new List<Status> ();

		animator = transform.GetComponent<Animator> ();
		controller = transform.GetComponent<Controller> ();
	}

	public void Update()
	{
		// Terminate Update if this Entity is paused
		if (!controller.PhysBody.simulated)
			return;

		// Update ability cooldowns
		foreach (Ability a in abilities)
		{
			if (a != null)
				a.OnUpdate (Time.deltaTime);
		}

		// Update passive effects
		foreach (Passive p in equipment)
		{
			if(p != null)
				p.OnUpdate(Time.deltaTime);
		}

		// Update status durations
		foreach (Status s in statusEffects)
		{
			if(s != null)
				s.OnUpdate (Time.deltaTime);
		}

		// Shield regeneration
		srDelayCurr -= Time.deltaTime;
		if (srDelayCurr <= 0f || shieldRegen < 0f)
			currentShield += shieldRegen;
	}

	// Play a death animation and take the entity out of play for some time
	public void die()
	{
		// Remove all statuses
		foreach (Status s in statusEffects)
		{
			if(s != null)
			{
				s.OnDeath();
				statusEffects.Remove(s);
			}
		}

		// Run OnDeath for passives
		foreach(Passive p in equipment)
		{
			if(p != null)
				p.OnDeath();
		}

		//TODO death effects, removal from game field, begin countdown to respawn
	}

	/* Events */

	// Entity takes damage
	public delegate void EntityDamaged();
	public event EntityDamaged entityDam;
	public virtual void OnEntityDamaged()
	{
		if(entityDam != null)
			entityDam();
	}

	// StatusList is updated
	public delegate void UpdatedStatusList(Status newStatus);
	public event UpdatedStatusList updateStatus;
	public virtual void OnStatusListUpdated(Status newStatus)
	{
		if(updateStatus != null)
			updateStatus(newStatus);
	}
}