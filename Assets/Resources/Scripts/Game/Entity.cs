using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
	[RequireComponent(typeof(Animator))]

	/* Instance Vars */
	private float shield;
	public float shieldMax;
	public float shieldRegen;
	public float shieldRegenDelay;

	private float health;
	public float healthMax;

	public int speed;

	public Faction faction;

	private EntityStats.Passive[] equipment;
	private List<EntityStats.Status> statusEffects;
	private EntityStats.Ability primaryFire;
	private EntityStats.Ability ability1;
	private EntityStats.Ability ability2;
	private EntityStats.Ability ability3;

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
		equipment = new EntityStats.Passive[4];
		statusEffects = new List<EntityStats.Status> ();

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