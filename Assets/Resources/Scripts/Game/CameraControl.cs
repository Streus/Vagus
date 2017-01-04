using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public static CameraControl mainCam;

	private Transform cam;

	//camera shake varibles
	private float intensity;
	private float shakeTime;

	public void Awake ()
	{
		if(mainCam == null)
			mainCam = this;
		Player = null;

		cam = transform.GetChild (0);
	}

	public void Update ()
	{
		if(transform.parent == null)
			return;
		
		if (shakeTime > 0f) {
			Vector3 shakePos = Random.insideUnitSphere * intensity;
			cam.transform.localPosition = shakePos;
			shakeTime -= Time.deltaTime;
		} else {
			cam.transform.localPosition = Vector3.zero;
		}
	}

	public void shakeCamera(float intensity, float shakeTime)
	{
		this.intensity = intensity;
		this.shakeTime = shakeTime;
	}

	public GameObject Player
	{
		get{ return transform.parent.gameObject; }
		set
		{
			if(value == null && transform.parent != null)
			{
				transform.parent.DetachChildren();
				gameObject.SetActive(true);
				transform.position = Vector3.zero;
				transform.rotation = Quaternion.Euler(15f, 0f, 0f);
			}
			else if(value != null)
			{
				transform.SetParent(value.transform, false);
				transform.position += new Vector3(0f, 2f, -2.5f);
			}
		}
	}
}
