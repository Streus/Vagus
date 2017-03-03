using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodeHighlight : MonoBehaviour
{
	public CaptureNode targetNode;

	public Image teamIndicator;
	public Text nodeNameIndicator;

	public RectTransform parentRect;
	private RectTransform rect;

	private int team;

	public static NodeHighlight create(CaptureNode targetNode, Transform displayArea, int team)
	{
		GameObject go = (GameObject)Instantiate (
			                Resources.Load<GameObject> ("Prefabs/UI/NodeHighlight"),
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
		teamIndicator.color = Bullet.factionColor((Faction)team);
		this.team = team;
	}

	public void FixedUpdate()
	{
		if (targetNode != null)
		{
			Vector3 screenCoords = Camera.main.WorldToScreenPoint (targetNode.transform.position);

			Vector3 convPos = Vector3.zero;
			RectTransformUtility.ScreenPointToWorldPointInRectangle (parentRect, (Vector2)screenCoords, Camera.main, out convPos);
			rect.position = convPos;
		}
		else
		{
			Destroy (gameObject);
		}
	}
}
