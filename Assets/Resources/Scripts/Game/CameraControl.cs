using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;
	private Transform cam;

	//camera shake varibles
	private float intensity;
	private float shakeTime;

	public void Start ()
	{
		//GameManager.cameraController = this;

		//player = GameManager.player;
		offset = transform.position - player.transform.position;

		cam = transform.GetChild (0);
	}

	public void Update ()
	{
		if (shakeTime > 0f) {
			Vector3 shakePos = Random.insideUnitSphere * intensity;
			cam.transform.localPosition = shakePos;
			shakeTime -= Time.deltaTime;
		} else {
			cam.transform.localPosition = Vector3.zero;
		}
	}
		
	public void LateUpdate ()
	{
		if (player != null) {
			Vector3 newPos = player.transform.position + offset;
			transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
		}
	}

	public void shakeCamera(float intensity, float shakeTime)
	{
		this.intensity = intensity;
		this.shakeTime = shakeTime;
	}
}
