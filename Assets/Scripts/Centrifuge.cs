using System;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
	private CentrifugeSlot[] _centrifugeSlots;

	private void Awake()
	{
		_centrifugeSlots = GetComponentsInChildren<CentrifugeSlot>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag("BloodTube")) return;
		
		Debug.Log("Sample in centrifuge found");
		foreach (var centrifugeSlot in _centrifugeSlots)
		{
			if (centrifugeSlot.BloodSample is not null) continue;
			
			centrifugeSlot.BloodSample = other.gameObject.GetComponent<BloodSample>();
			break;
		}
	}

	public void SeparateSamples()
	{
		foreach (var centrifugeSlot in _centrifugeSlots)
		{
			centrifugeSlot.SeparateSample();
		}
		Debug.Log("Samples separated");
	}
}