using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Dropper : MonoBehaviour
{
    public DropperContent Content;
    public BloodClass BloodClass;
    private GameObject _target;

    private void Awake()
    {
        GetComponent<XRGrabInteractable>().activated.AddListener(Drop);
    }

    private void Drop(ActivateEventArgs arg0)
    {
        if (_target is null) return;

        _target.GetComponent<TabletHole>().Fill(Content, BloodClass);
        Content = DropperContent.None;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tablet Hole"))
        {
            _target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tablet Hole"))
        {
            _target = null;
        }
    }
}
