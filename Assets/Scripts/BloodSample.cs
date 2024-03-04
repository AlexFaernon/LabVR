using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BloodSample : MonoBehaviour
{
    public BloodType BloodType { get; private set; }
    public bool Rh { get; private set; }
    [SerializeField] private GameObject notSeparatedContent;
    [SerializeField] private GameObject separatedContent;
    [SerializeField] private GameObject cap;
    public bool isSeparated;
    private IEnumerable<Collider> _contentColliders;

    private void Awake()
    {
        _contentColliders = GetComponentsInChildren<Collider>().Where(collider => collider.isTrigger);
        foreach (var contentCollider in _contentColliders)
        {
            contentCollider.enabled = false;
        }
        GetComponent<XRGrabInteractable>().activated.AddListener(SwitchCap);
    }

    private void Update()
    {
        notSeparatedContent.SetActive(!isSeparated);
        separatedContent.SetActive(isSeparated);
    }

    private void SwitchCap(ActivateEventArgs arg0)
    {
        cap.SetActive(!cap.activeSelf);
        foreach (var contentCollider in _contentColliders)
        {
            contentCollider.enabled = !cap.activeSelf;
        }
    }
}
