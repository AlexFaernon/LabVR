using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BloodSample : MonoBehaviour
{
    public BloodClass BloodClass;
    public BloodClass AssumedBloodClass;
    [SerializeField] private GameObject notSeparatedContent;
    [SerializeField] private GameObject separatedContent;
    [SerializeField] private GameObject cap;
    public bool isSeparated;
    private IEnumerable<Collider> _contentColliders;
    private TMP_Text[] _labels;

    private void Awake()
    {
        _contentColliders = GetComponentsInChildren<Collider>().Where(collider1 => collider1.isTrigger);
        foreach (var contentCollider in _contentColliders)
        {
            contentCollider.enabled = false;
        }
        GetComponent<XRGrabInteractable>().activated.AddListener(SwitchCap);
        _labels = GetComponentsInChildren<TMP_Text>();
        foreach (var label in _labels)
        {
            label.gameObject.SetActive(false);
        }
    }
    
    private void Update()
    {
        notSeparatedContent.SetActive(!isSeparated);
        separatedContent.SetActive(isSeparated);
        if (AssumedBloodClass is null) return;

        foreach (var label in _labels)
        {
            label.gameObject.SetActive(true);
            label.text = $"{AssumedBloodClass.BloodType} Rh{(AssumedBloodClass.Rh ? "+" : "-")}";
        }
    }

    private void SwitchCap(ActivateEventArgs arg0)
    {
        cap.SetActive(!cap.activeSelf);
        foreach (var contentCollider in _contentColliders)
        {
            contentCollider.enabled = !cap.activeSelf;
        }
    }
    
    private void Start()
    {
        Debug.Log($"Blood sample group:{BloodClass.BloodType} rh:{BloodClass.Rh}");
    }
}
