using UnityEngine;
using System.Collections;

public class Singularity : MonoBehaviour
{
	public float forceMagnitude;

	public void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "GravAffected")
		{
			Vector3 dv = transform.position - col.transform.position;
			float distance = Mathf.Sqrt((dv.x * dv.x) + (dv.y * dv.y) + (dv.z * dv.z));

			float gravForce = 1 / (distance * distance);
			col.gameObject.GetComponent<Rigidbody>().AddForce(dv * gravForce, ForceMode.Acceleration);
		}
	}
}
