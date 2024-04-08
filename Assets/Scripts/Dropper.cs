using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class Dropper : MonoBehaviour
{
    public int dropsLeft = int.MaxValue;
    public DropperContent content;
    public BloodClass BloodClass;
    private GameObject _target;

    private void Awake()
    {
        GetComponent<XRGrabInteractable>().activated.AddListener(Drop);
    }

    private void Drop(ActivateEventArgs arg0)
    {
        if (_target is null) return;

        _target.GetComponent<TabletHole>().Fill(content, BloodClass);
        dropsLeft--;
        if (dropsLeft == 0)
        {
            content = DropperContent.None;
        }
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
