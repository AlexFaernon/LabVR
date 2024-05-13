using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromFloor : MonoBehaviour
{
    private Vector3 _initialPos;
    void Start()
    {
        _initialPos = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Floor")) return;

        transform.position = _initialPos;
    }
}
