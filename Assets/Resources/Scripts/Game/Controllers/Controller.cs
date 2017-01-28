using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour
{
	/* Instance Vars */
	protected Entity self;
	protected Rigidbody2D physbody;

	/* Accessors */
	public Entity Self
	{
		get{ return self; }
	}

	public Rigidbody2D PhysBody
	{
		get{ return physbody; }
	}

	/* Instance Methods */
	public virtual void Awake()
	{
		self = transform.GetComponent<Entity> ();
		physbody = transform.GetComponent<Rigidbody2D> ();
	}
}
