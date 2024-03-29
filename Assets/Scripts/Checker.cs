using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Checker : MonoBehaviour
{
    [SerializeField] private float nullSampleEjectSpeed;
    private ResultList _resultList;

    private void Awake()
    {
        _resultList = GetComponentInChildren<ResultList>();
        _resultList.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BloodTube")) return;

        var bloodSample = other.GetComponentInParent<BloodSample>();
        if (bloodSample.AssumedBloodClass is null)
        {
            bloodSample.GetComponent<Rigidbody>().velocity = new Vector3(-0.5f,2,-0.5f) * nullSampleEjectSpeed;
            return;
        }
        _resultList.gameObject.SetActive(true);
        _resultList.AddResult(bloodSample);
        Destroy(bloodSample.GetComponent<XRGrabInteractable>());
    }
}
