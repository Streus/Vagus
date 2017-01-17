using UnityEngine;
using System.Collections;

public class Player : Controller
{
	public float xSensitivity;
	public float ySensitivity;

	public override void OnStartLocalPlayer()
	{
		CameraControl.mainCam.Player = gameObject;
		Debug.Log(CameraControl.mainCam.Player.name + " is being followed by the camera.");
	}

	public override void Awake()
	{
		base.Awake();
		xSensitivity = 10f;
		ySensitivity = 10f;
	}

	public void Update()
	{
		float dx = -xSensitivity * Input.GetAxis("Mouse X");
		//float dy = -ySensitivity * Input.GetAxis("Mouse Y");
		transform.Rotate(0, 0, dx);
	}

	// Movement
	public void FixedUpdate()
	{
		if(!isLocalPlayer)
			return;

		//temporary 
		if (Input.GetKey (KeyCode.W))
			physbody.AddForce (transform.up * self.Speed);
		if (Input.GetKey (KeyCode.A))
			physbody.AddForce (transform.right * -self.Speed);
		if (Input.GetKey (KeyCode.S))
			physbody.AddForce (transform.up * -self.Speed);
		if (Input.GetKey (KeyCode.D))
			physbody.AddForce (transform.right * self.Speed);
	}

	public void OnDestroy()
	{
		string str = CameraControl.mainCam.Player.name;
		CameraControl.mainCam.Player = null;
		Debug.Log(str + " reliquished control of the camera.");
	}
}
