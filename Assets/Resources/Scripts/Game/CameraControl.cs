using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public static CameraControl mainCam;

	public Vector3 camOffset;
	public Vector3 camRotation;

	private Transform cam;

	//camera shake varibles
	private float intensity;
	private float shakeTime;

	private GameObject followTar;
	public GameObject FollowTarget
	{
		get{ return followTar; }
		set{ followTar = value; }
	}

	public void Awake()
	{
		if (mainCam == null)
			mainCam = this;
		else
			throw new UnityException ("More than one camera active!");
		
		FollowTarget = null;

		cam = transform.GetChild (0);
	}

	public void Start()
	{
		cam.position = camOffset;
		cam.rotation = Quaternion.Euler(camRotation);
	}

	public void Update()
	{
		if (shakeTime > 0f) {
			Vector3 shakePos = Random.insideUnitSphere * intensity;
			cam.localPosition = shakePos + camOffset;
			shakeTime -= Time.deltaTime;
		} else {
			cam.localPosition = camOffset;
		}
	}

	public void LateUpdate()
	{
		if (followTar == null)
			return;
		
		transform.position = followTar.transform.position;
		transform.rotation = Quaternion.Euler(0f, 0f, followTar.transform.rotation.eulerAngles.z);
	}

	public void shakeCamera(float intensity, float shakeTime)
	{
		this.intensity = intensity;
		this.shakeTime = shakeTime;
	}
}
