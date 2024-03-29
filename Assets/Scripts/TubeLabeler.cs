using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TubeLabeler : MonoBehaviour
{
    [SerializeField] private Transform attachPoint;
    public BloodType labelBloodType;
    public bool labelRh;
    public BloodSample BloodSample { private set; get; }
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("BloodTube")) return;

        BloodSample = other.gameObject.GetComponent<BloodSample>();
        BloodSample.GetComponent<ObjectAttacher>().AttachToObject(attachPoint, _rigidbody, DetachSample);
    }

    private void DetachSample()
    {
        BloodSample.AssumedBloodClass = new BloodClass(labelBloodType, labelRh);
        BloodSample = null;
    }
}
