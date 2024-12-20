﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class TubeSlot : MonoBehaviour
{
	private BloodSample _bloodSample;
	private Rigidbody _parentRigidbody;
	public BloodSample BloodSample
	{
		get => _bloodSample;
		set
		{
			_bloodSample = value;
			if (_bloodSample is null) return;
			
			_bloodSample.GetComponent<ObjectAttacher>().AttachToObject(transform, _parentRigidbody, DetachSample);
		}
	}

	private void Awake()
	{
		_parentRigidbody = GetComponentInParent<Rigidbody>();
	}

	private void DetachSample()
	{
		BloodSample = null;
	}
}
