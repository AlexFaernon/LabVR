using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class InfoTablet : MonoBehaviour
{
	[SerializeField] private InputActionReference switchPages;
	[SerializeField] private Transform pagesParent;

	private int _currentPageNumber;
	private DynamicMoveProvider _moveProvider;
	private ActionBasedSnapTurnProvider _snapTurnProvider;
	private XRGrabInteractable _grabInteractable;
	private bool IsGrabbedByRightController => _grabInteractable.firstInteractorSelecting?.transform.parent.name == "Right Controller";
	private bool IsGrabbedByLeftController => _grabInteractable.firstInteractorSelecting?.transform.parent.name == "Left Controller";

	public int CurrentPageNumber
	{
		get => _currentPageNumber;
		set
		{
			_currentPageNumber = value;
			foreach (Transform page in pagesParent)
			{
				page.gameObject.SetActive(page.GetSiblingIndex() == CurrentPageNumber);
			}
		}
	}


	private void Awake()
	{
		_moveProvider = FindObjectOfType<DynamicMoveProvider>();
		_snapTurnProvider = FindObjectOfType<ActionBasedSnapTurnProvider>();
		_grabInteractable = GetComponent<XRGrabInteractable>();
		
		switchPages.action.started += SwitchPage;
		_grabInteractable.selectEntered.AddListener(TurnOffMovementOnGrab);
		_grabInteractable.selectExited.AddListener(TurnOnMovementOffGrab);

		CurrentPageNumber = 0;
	}

	private void TurnOffMovementOnGrab(SelectEnterEventArgs _)
	{
		if (IsGrabbedByRightController)
		{
			_snapTurnProvider.enabled = false;
		}
		else if (IsGrabbedByLeftController)
		{
			_moveProvider.enabled = false;
		}
		else
		{
			throw new UnityException("Grabbed by unknown controller");
		}
	}

	private void TurnOnMovementOffGrab(SelectExitEventArgs _)
	{
		_snapTurnProvider.enabled = true;
		_moveProvider.enabled = true;
	}
	
	private void SwitchPage(InputAction.CallbackContext context)
	{
		var value = context.ReadValue<Vector2>().x;
		if (value > 0.8f && CurrentPageNumber < pagesParent.childCount - 1)
		{
			CurrentPageNumber++;
		}
		else if (value < -0.8 && CurrentPageNumber > 0)
		{
			CurrentPageNumber--;
		}
	}

	private void OnDestroy()
	{
		switchPages.action.started -= SwitchPage;
	}
}
