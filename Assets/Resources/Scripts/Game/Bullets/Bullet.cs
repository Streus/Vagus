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

	// Deal damage to non-aligned faction entities and destructable objects
	public static void dealDamage(float damage, Entity other)
	{
		if(damage < 0)
		{
			throw new UnityException("Tried to apply negative damage!");
		}
		other.Shield -= damage;
		if(damage > other.Shield)
			other.Health -= (damage - other.Shield);
	}

	/* Instance Methods */

	public void Awake()
	{
		physbody = transform.GetComponent<Rigidbody>();
	}
		
	public void OnTriggerEnter(Collision col)
	{
		// Collision with an entity gameobject
		if(col.gameObject.layer == LayerMask.NameToLayer("Entity"))
		{
			Entity other = col.gameObject.GetComponent<Entity>();
			dealDamage(damage, other);
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