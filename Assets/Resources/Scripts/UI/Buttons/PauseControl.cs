using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
	/* Instance Vars */
	public GameObject nodeOverlay;
	public Button toggleNodeEdit;

	public Menu parentMenu;

	private bool inStratView;
	private bool editingNodes;

	private Transform dragObject;
	private Plane dragPlane;
	private Vector3 dragStart;

	/* Instance Methods */
	public void Start()
	{
		inStratView = editingNodes = false;
		dragObject = null;
		dragPlane = new Plane ();
		dragStart = Vector3.zero;

		parentMenu.changedFocus += new Menu.FocusChanged(menuChangedFocus);

		//fill the nodeOverlay with NodeHighlights
		for (int i = 0; i < GameManager.manager.nodes.Length; i++)
		{
			CaptureNode node = GameManager.manager.nodes [i];
			if (node != null)
			{
				NodeHighlight.create (node, nodeOverlay.transform, node.team);
			}
		}
		nodeOverlay.SetActive (false);
	}

	public void Update()
	{
		toggleNodeEdit.interactable = GameManager.manager.canEditNodes;

		if (inStratView && editingNodes)
		{
			if (Input.GetKeyDown (KeyCode.Mouse0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 500f))
				{
					CaptureNode cn = hit.transform.parent.GetComponent<CaptureNode> ();
					if (cn != null) //&& is localPlayer's node
					{
						dragObject = hit.transform.parent;
						dragPlane = new Plane (Vector3.forward, dragObject.position);
						dragStart = dragObject.position;
					}
				}
			}
			else if (Input.GetKeyUp (KeyCode.Mouse0))
			{
				dragObject = null;
			}

			if (dragObject != null)
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float intersectionPoint;
				if (dragPlane.Raycast (ray, out intersectionPoint))
				{
					Vector3 unclampedPos = ray.GetPoint (intersectionPoint);
					Vector3 clampedPos = new Vector3 (
						50f * Mathf.Round (unclampedPos.x / 50f), 
						50f * Mathf.Round (unclampedPos.y / 50f));
					
					float distFromOrigin = Mathf.Sqrt ((clampedPos.x * clampedPos.x) + (clampedPos.y * clampedPos.y));
					if (50f < distFromOrigin && distFromOrigin < 260f && Mathf.Sign(clampedPos.y) == Mathf.Sign(dragStart.y) && clampedPos.y != 0)
						dragObject.position = clampedPos;
				}
				Debug.Log ("Dragging: " + dragObject.gameObject.name + "\n" + dragObject.position.ToString());
			}
		}
	}

	public void toggleStratView()
	{
		if (inStratView)
		{
			CameraControl.mainCam.allowPan = false;
			CameraControl.mainCam.snapToTarget ();
			CameraControl.mainCam.changeCamPosition (CameraControl.playProfile);
			nodeOverlay.SetActive (false);
		}
		else
		{
			CameraControl.mainCam.allowPan = true;
			CameraControl.mainCam.snapToTarget ();
			CameraControl.mainCam.changeCamPosition (CameraControl.stratProfile);
			nodeOverlay.SetActive (true);
		}
		inStratView = !inStratView;
	}

	public void toggleEditingNodes()
	{
		editingNodes = !editingNodes;
	}

	public void menuChangedFocus(bool inFocus)
	{
		if (!inFocus && inStratView)
			toggleStratView ();
	}
}
