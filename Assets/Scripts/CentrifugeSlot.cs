using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CentrifugeSlot : MonoBehaviour
{
	private BloodSample _bloodSample;
	private Rigidbody _centrifugeRigidbody;
	public BloodSample BloodSample
	{
		get => _bloodSample;
		set
		{
			_bloodSample = value;
			if (_bloodSample is null) return;
			
			_bloodSample.GetComponent<ObjectAttacher>().AttachToObject(transform, _centrifugeRigidbody, DetachSample);
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

	private void DetachSample()
	{
		BloodSample = null;
	}
}
