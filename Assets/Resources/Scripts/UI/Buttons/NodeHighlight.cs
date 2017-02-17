using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodeHighlight : MonoBehaviour
{
	public GameObject targetNode;

	public Image teamIndicator;
	public Text nodeNameIndicator;

	public RectTransform parentRect;
	private RectTransform rect;

	private int team;

	public static NodeHighlight create(GameObject targetNode, Transform displayArea, int team)
	{
		GameObject go = (GameObject)Instantiate (
			                Resources.Load<GameObject> ("Prefabs/UI/Buttons/NodeHighlight"),
			                displayArea, false);
		NodeHighlight nh = go.GetComponent<NodeHighlight> ();
		nh.parentRect = displayArea.GetComponent<RectTransform> ();
		nh.targetNode = targetNode;
		nh.nodeNameIndicator.text = targetNode.name;
		nh.changeTeam (team);

		return nh;
	}

	public void Awake()
	{
		rect = GetComponent<RectTransform> ();
	}

	public void changeTeam(int team)
	{
		Color color = Color.white;
		switch (team)
		{
		case 0:
			color = new Color(1f, 0.5f, 0f, 1f);
			break;
		case 1:
			color = Color.blue;
			break;
		}
		teamIndicator.color = color;
	}

	public void LateUpdate()
	{
		if (targetNode != null)
		{
			Vector3 targetCoords = Camera.main.WorldToScreenPoint (targetNode.transform.position);
			Vector2 convPosition = new Vector2 (
				                       (targetCoords.x * parentRect.sizeDelta.x) - (parentRect.sizeDelta.x * 0.5f),
				                       (targetCoords.y * parentRect.sizeDelta.y) - (parentRect.sizeDelta.y * 0.5f));
			rect.anchoredPosition = convPosition;
		}
		else
		{
			Destroy (gameObject);
		}
	}
}
