using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public static CameraControl mainCam;

	public static CameraProfile playProfile = 
		new CameraProfile (
			new Vector3 (0f, -10f, -5f),
			new Vector3 (-75f, 0f, 0f), true);

	public static CameraProfile stratProfile = 
		new CameraProfile (
			new Vector3 (0f, 0f, -300f), 
			new Vector3 (0f, 0f, 0f), false);

	/* Instance Vars */

	//camera child variables
	private CameraProfile camProfile;

	private Transform cam;

	//camera shake varibles
	private float intensity;
	private float shakeTime;
	private Vector3 shakeOffset;

	//camera pan variables
	public bool allowPan;

	private float panBorderSize;
	private float pan_xMin;
	private float pan_xMax;
	private float pan_yMin;
	private float pan_yMax;
	public float PanBorderSize
	{
		get{ return panBorderSize; }
		set
		{
			panBorderSize = Mathf.Clamp(value, 0.01f, 0.5f);
			pan_xMin = Screen.width * panBorderSize;
			pan_xMax = Screen.width - (Screen.width * panBorderSize);
			pan_yMin = Screen.height * panBorderSize;
			pan_yMax = Screen.height - (Screen.height * panBorderSize);
		}
	}

	public float panSpeed;
	private Vector3 panOffset;

	private GameObject followTar;
	public GameObject FollowTarget
	{
		get{ return followTar; }
		set{ followTar = value; }
	}

	/* Instance Methods */
	public void Awake()
	{
		if (mainCam == null)
			mainCam = this;
		else
			throw new UnityException ("More than one camera active!");
		
		FollowTarget = null;

		cam = transform.GetChild (0);
		cam.localRotation = camProfile.rotation;

		panOffset = shakeOffset = Vector3.zero;
	}

	public void Start()
	{
		snapToTarget ();
		PanBorderSize = 0.1f; //DEBUG CODE
		panSpeed = 1.5f; //DEBUG CODE
	}

	public void Update()
	{
		//interpolate rotation changes
		cam.localRotation = Quaternion.Lerp(cam.localRotation, camProfile.rotation, Time.deltaTime * 15f);

		//shake
		if (shakeTime > 0f)
		{
			shakeOffset = Random.insideUnitSphere * intensity;
			shakeTime -= Time.deltaTime;
		}
		else
		{
			shakeOffset = Vector3.zero;
		}

		//pan
		if (allowPan)
		{
			Vector2 mousePos = Input.mousePosition;
			if (mousePos.x < pan_xMin)
				panOffset -= cam.transform.right * panSpeed;
			if (mousePos.x > pan_xMax)
				panOffset += cam.transform.right * panSpeed;
			if (mousePos.y < pan_yMin)
				panOffset -= cam.transform.up * panSpeed;
			if (mousePos.y > pan_yMax)
				panOffset += cam.transform.up * panSpeed;
		}

		cam.localPosition = shakeOffset + camProfile.offset + panOffset;
	}

	public void LateUpdate()
	{
		if (followTar == null)
			return;
		
		transform.position = followTar.transform.position;
		if (camProfile.followTargetRotation)
			transform.rotation = Quaternion.Euler (0f, 0f, followTar.transform.rotation.eulerAngles.z);
		else
			transform.rotation = Quaternion.identity;
	}

	public void shakeCamera(float intensity, float shakeTime)
	{
		this.intensity = intensity;
		this.shakeTime = shakeTime;
	}

	public void snapToTarget()
	{
		panOffset = Vector3.zero;
	}

	public void changeCamPosition(CameraProfile newProfile)
	{
		camProfile = newProfile;
	}
	public void changeCamPosition(Vector3 newOffset, Vector3 newRotation, bool followTarRot)
	{
		changeCamPosition(new CameraProfile (newOffset, newRotation, followTarRot));
	}

	// Inner class that handles preset camera angles and positions
	public struct CameraProfile
	{
		public Vector3 offset;
		public Quaternion rotation;
		public bool followTargetRotation;

		public CameraProfile(Vector3 offset, Vector3 eR, bool followTargetRotation)
		{
			this.offset = offset;
			this.rotation = Quaternion.Euler(eR);
			this.followTargetRotation = followTargetRotation;
		}
	}
}
