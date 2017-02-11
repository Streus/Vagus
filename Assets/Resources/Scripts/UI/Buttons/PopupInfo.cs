using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupInfo : MonoBehaviour
{
	private Text textBox;
	private Rect rect;
	private Rect canvasRect;

	public void Awake()
	{
		textBox = transform.GetChild (0).GetComponent<Text> ();
		rect = GetComponent<RectTransform> ().rect;
	}

	public void changeText(string text)
	{
		textBox.text = text;
		repositionBox ();
	}

	private void repositionBox()
	{
		
	}
}
