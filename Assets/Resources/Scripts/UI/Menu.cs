using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	private Animator animator;
	private CanvasGroup canvasGroup;

	public bool IsOpen
	{
		get{ return animator.GetBool("IsOpen"); }
		set{ animator.SetBool("IsOpen", value); }
	}

	public void Awake()
	{
		animator = GetComponent<Animator>();
		canvasGroup = GetComponent<CanvasGroup>();

		//move this menu onto the canvas
		RectTransform rect = GetComponent<RectTransform>();
		rect.offsetMax = rect.offsetMin = Vector2.zero;
	}

	public void Update()
	{
		//if this menu is not open, make it uninteractable
		canvasGroup.blocksRaycasts = canvasGroup.interactable = animator.GetCurrentAnimatorStateInfo(0).IsName("Open");
	}
}
