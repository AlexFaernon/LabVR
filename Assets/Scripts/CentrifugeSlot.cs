using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CentrifugeSlot : MonoBehaviour
{
	private BloodSample _bloodSample;
	private FixedJoint _tubeJoint;
	private Rigidbody _centrifugeRigidbody;
	public BloodSample BloodSample
	{
		get => _bloodSample;
		set
		{
			_bloodSample = value;
			if (_bloodSample is null) return;
			
			_tubeJoint = _bloodSample.AddComponent<FixedJoint>();
			_tubeJoint.connectedAnchor = transform.position;
			_tubeJoint.connectedBody = _centrifugeRigidbody;
			_tubeJoint.enablePreprocessing = false;
			_bloodSample.GetComponent<Rigidbody>().isKinematic = true;
			_bloodSample.transform.position = transform.position;
			_bloodSample.GetComponent<XRGrabInteractable>().selectEntered.AddListener(DetachSample);
		}
	}

	private void Awake()
	{
		_centrifugeRigidbody = GetComponentInParent<Rigidbody>();
	}
	
	public void SeparateSample()
	{
		if (BloodSample is not null)
		{
			BloodSample.isSeparated = true;
		}
	}

	private void DetachSample(SelectEnterEventArgs arg0)
	{
		BloodSample.GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(DetachSample);
		Destroy(_tubeJoint);
		_bloodSample.GetComponent<Rigidbody>().isKinematic = false;
		BloodSample = null;
		_tubeJoint = null;
	}
}
