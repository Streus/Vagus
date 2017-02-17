using UnityEngine;
using System.Collections;

public class Player : Controller
{
	public float xSensitivity;
	public float ySensitivity;

	//double tap variables

	private GameObject engEmmisEff;

	public override void OnStartLocalPlayer()
	{
		CameraControl.mainCam.FollowTarget = gameObject;
		CameraControl.mainCam.changeCamPosition (CameraControl.playProfile);
		Debug.Log(CameraControl.mainCam.FollowTarget.name + " is being followed by the camera.");
		HUDControl.hud.subject = self;
	}

	public override void Awake()
	{
		base.Awake();
		xSensitivity = 10f;
		ySensitivity = 10f;

		engEmmisEff = transform.GetChild (0).gameObject;
		engEmmisEff.SetActive (false);
	}

	public void Update()
	{
		if(isNotInControl())
			return;
		
		float dx = -xSensitivity * Input.GetAxis("Mouse X");
		//float dy = -ySensitivity * Input.GetAxis("Mouse Y");
		transform.Rotate(0, 0, dx);
	}

	// Movement
	public void FixedUpdate()
	{
		if(isNotInControl())
			return;

		//temporary 
		if (Input.GetKey (KeyCode.W))
			physbody.AddForce (transform.up * self.speed);
		if (Input.GetKey (KeyCode.A))
			physbody.AddForce (transform.right * -self.speed);
		if (Input.GetKey (KeyCode.S))
			physbody.AddForce (transform.up * -self.speed);
		if (Input.GetKey (KeyCode.D))
			physbody.AddForce (transform.right * self.speed);

		engEmmisEff.SetActive (Input.GetKey (KeyCode.W));
	}

	public bool isNotInControl()
	{
		return !isLocalPlayer ||
			GameManager.manager.inPauseMenu ||
			GameManager.manager.gameState == (int)GameState.postgame;
	}

	public void OnDestroy()
	{
		string str = CameraControl.mainCam.FollowTarget.name;
		CameraControl.mainCam.FollowTarget = null;
		Debug.Log(str + " reliquished control of the camera.");
	}
}
