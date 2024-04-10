using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TubeLabeler : MonoBehaviour
{
    public BloodType labelBloodType;
    public bool labelRh;
    private TubeSlot _tubeSlot;
    public BloodSample BloodSample => _tubeSlot.BloodSample;

    private void Awake()
    {
        _tubeSlot = GetComponentInChildren<TubeSlot>();
    }

    private void Update()
    {
        if (BloodSample is not null)
        {
            BloodSample.AssumedBloodClass = new BloodClass(labelBloodType, labelRh);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("BloodTube")) return;

        _tubeSlot.BloodSample = other.gameObject.GetComponent<BloodSample>();
    }
}
