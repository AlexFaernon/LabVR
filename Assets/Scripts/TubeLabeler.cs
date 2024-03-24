using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeLabeler : MonoBehaviour
{
    [SerializeField] private Transform attachPoint;
    public BloodType labelBloodType;
    public bool labelRh;
    private BloodSample _bloodSample;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("BloodTube")) return;

        _bloodSample = other.gameObject.GetComponent<BloodSample>();
        _bloodSample.GetComponent<ObjectAttacher>().AttachToObject(attachPoint, _rigidbody, DetachSample);
    }

    private void DetachSample()
    {
        _bloodSample.AssumedBloodClass = new BloodClass(labelBloodType, labelRh);
        _bloodSample = null;
    }
}
