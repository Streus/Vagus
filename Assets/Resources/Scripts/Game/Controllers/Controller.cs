using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	protected Entity self;
	protected Rigidbody physbody;

	public void Awake()
	{
		self = transform.GetComponent<Entity> ();
		physbody = transform.GetComponent<Rigidbody> ();
	}
}
