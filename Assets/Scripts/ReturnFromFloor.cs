using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromFloor : MonoBehaviour
{
    private Vector3 _initialPos;
    private Rigidbody _rigidbody;
    void Start()
    {
        _initialPos = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Floor")) return;
        
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initialPos;
    }
}
