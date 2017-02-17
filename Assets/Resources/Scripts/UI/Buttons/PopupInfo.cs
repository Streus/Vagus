using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupInfo : MonoBehaviour
{
	private Text textBox;
	private RectTransform self;
	private RectTransform canvas;

	public static PopupInfo createPopup(string text, Canvas canvas, Vector3 position)
	{
		GameObject pu = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/Popup Info"), canvas.transform, false);
		pu.transform.position = position;
		PopupInfo popup = pu.GetComponent<PopupInfo> ();
		popup.changeText (text);

		return popup;
	}

	public void Awake()
	{
		textBox = transform.GetChild (0).GetComponent<Text> ();
		self = GetComponent<RectTransform> ();
		canvas = transform.root.GetComponent<RectTransform> ();
	}

	public void changeText(string text)
	{
		textBox.text = text;
		repositionBox ();
	}

	private void repositionBox()
	{
		//get bounds of canvas and this object's recttransform
		Vector3[] canBounds = new Vector3[4];
		canvas.GetLocalCorners (canBounds);
		Vector3[] selfBounds = new Vector3[4];
		self.GetLocalCorners (selfBounds);

		//put recttransform's bounds into terms of the canvas's coordinate system
		for (int i = 0; i < selfBounds.Length; i++)
		{
			selfBounds [i] += self.localPosition;
		}

		float dx = 0f;
		float dy = 0f;

		if (selfBounds [0].x < canBounds [0].x)
			dx -= selfBounds [0].x - canBounds [0].x;
		if (selfBounds [2].x > canBounds [2].x)
			dx -= selfBounds [2].x - canBounds [2].x;

		if (selfBounds [0].y < canBounds [0].y)
			dy -= selfBounds [0].y - canBounds [0].y;
		if (selfBounds [2].y > canBounds [2].y)
			dy -= selfBounds [2].y - canBounds [2].y;

		//shift the localPosition of this object's recttransform ot bring it within the canvas's local bounds
		self.localPosition = new Vector3 (self.localPosition.x + dx, self.localPosition.y + dy, self.localPosition.z);
	}
}
