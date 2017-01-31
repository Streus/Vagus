using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour
{
	/* Instance Vars */
	public Faction faction;
	public float damage;
	public int initSpeed;
	public float duration;
	public bool onHit;

	[HideInInspector]
	public GameObject creator;
	[HideInInspector]
	public Rigidbody physbody;

	/* Static Methods */

	// Create a bullet prefab, initialize its Bullet script, and give it velocity
	public static GameObject createBullet(GameObject prefab, GameObject creator, Vector3 position, Quaternion rotation)
	{
		// Create bullet prefab
		GameObject b = (GameObject)Instantiate(prefab, position, rotation);
		Bullet bullet = b.GetComponent<Bullet>();

		// Exceptions
		if(bullet == null)
		{
			DestroyImmediate(b);
			throw new UnityException("Tried to shoot something that wasn't a bullet!");
		}
		if(creator == null)
		{
			DestroyImmediate(b);
			throw new UnityException("Created a bullet without assigning a creator!");
		}

		// Switch the faction to the creator's faction, if it has one
		Entity origin = creator.GetComponent<Entity>();
		if(origin != null)
			bullet.faction = origin.faction;

		// Add velocity to the bullet
		bullet.physbody.AddForce(bullet.transform.forward * bullet.initSpeed, ForceMode.Impulse);

		// Spawn the bullet on the network
		NetworkServer.Spawn(b);

		return b;
	}

	// Thow out a raycast and deal damage to either the first hit Entity, or all Entities hit
	public static void hitScan(Faction faction, float damage, Vector2 dir, Vector2 start, float distance, int penetrations = int.MaxValue)
	{
		//TODO work OnDamageTaken and OnDamageDealt in here somehow
		int worldMask = 1 << 8;
		RaycastHit2D[] hits = Physics2D.RaycastAll (start, dir, distance, worldMask);
		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider != null && hit.collider.GetComponent<Entity> ().faction != faction)
			{
				dealDamage(damage, hit.collider.GetComponent<Entity> ());
				penetrations--;
				if (penetrations < 0)
					return;
			}
		}
	}

	// Deal damage to non-aligned faction entities and destructable objects
	public static void dealDamage(Bullet bullet, Entity other)
	{
		dealDamage (bullet.damage, other);
	}
	public static void dealDamage(float damage, Entity other)
	{
		if(damage < 0)
		{
			throw new UnityException("Tried to apply negative damage!");
		}
		other.currentShield -= damage;
		other.resetSRDelay ();
		if(damage > other.currentShield)
			other.currentHealth -= (damage - other.currentShield);

		other.OnEntityDamaged();
	}

	/* Instance Methods */
	public void Awake()
	{
		physbody = transform.GetComponent<Rigidbody>();
	}
		
	public void OnTriggerEnter(Collider col)
	{
		// Collision with an entity gameobject
		if(col.gameObject.layer == LayerMask.NameToLayer("Entity"))
		{
			Entity other = col.gameObject.GetComponent<Entity>();
			dealDamage(damage, other);

			foreach (Passive p in other.equipment)
			{
				p.OnDamageTaken (this);
			}

			Entity creatorEnt = creator.GetComponent<Entity> ();
			if(creatorEnt != null)
			{
				foreach (Passive p in creatorEnt.equipment)
				{
					p.OnDamageDealt (this, other);
				}
			}
		}
	}
}

/* Used to determine whether damage is applied on Bullet-Entity collision */
public enum Faction
{
	neutral,
	player_1,
	player_2,
	player_3,
	player_4,
	enemy
}