using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour
{
	protected Entity self;
	protected Rigidbody2D physbody;

	public virtual void Awake()
	{
		self = transform.GetComponent<Entity> ();
		physbody = transform.GetComponent<Rigidbody2D> ();
	}
}
