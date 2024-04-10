using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectAttacher : MonoBehaviour
{
    private FixedJoint _fixedJoint;
    private Rigidbody _rigidbody;
    private Action _onDetach;

    private void Awake()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(Detach);
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AttachToObject(Transform transformAttach, Rigidbody rigidbodyAttach, Action onDetach)
    {
        if (_fixedJoint is null)
        {
            _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = rigidbodyAttach;
            _rigidbody.isKinematic = true;
        }
        
        transform.position = transformAttach.position;
        transform.rotation = transformAttach.rotation;
        _onDetach = onDetach;
    }

    private void Detach(SelectEnterEventArgs arg0)
    {
        if (_fixedJoint is null) return;
        
        _onDetach();
        _onDetach = null;
        Destroy(_fixedJoint);
        _fixedJoint = null;
        _rigidbody.isKinematic = false;
    }
}
