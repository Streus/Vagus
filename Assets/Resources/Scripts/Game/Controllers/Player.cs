using UnityEngine;
using System.Collections;

public class Player : Controller
{
	// Movement
	public void FixedUpdate ()
	{
		//temporary 
		if (Input.GetKey (KeyCode.W))
			physbody.AddForce (Vector3.forward * self.Speed);
		if (Input.GetKey (KeyCode.A))
			physbody.AddForce (Vector3.left * self.Speed);
		if (Input.GetKey (KeyCode.S))
			physbody.AddForce (Vector3.back * self.Speed);
		if (Input.GetKey (KeyCode.D))
			physbody.AddForce (Vector3.right * self.Speed);

		if (Input.GetKey (KeyCode.Space))
		{
			RaycastHit hit;
			Ray ray = new Ray (transform.position + Vector3.down, Vector3.down);
			GetComponent<Collider> ().Raycast (ray, out hit, 0.1f);
			if(hit.collider != null)
				physbody.AddForce (Vector3.up * 2, ForceMode.Impulse);
		}
	}
}
