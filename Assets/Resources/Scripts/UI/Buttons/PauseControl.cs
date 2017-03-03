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

	/* Instance Methods */
	public void Start()
	{
		inStratView = false;

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

	public void menuChangedFocus(bool inFocus)
	{
		if (!inFocus && inStratView)
			toggleStratView ();
	}
}
