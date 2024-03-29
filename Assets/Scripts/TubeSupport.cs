using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubeSupport : MonoBehaviour
{
    [SerializeField] private GameObject bloodSamplePrefab;
    [SerializeField] private Toggle toggle0;
    [SerializeField] private Toggle toggleA;
    [SerializeField] private Toggle toggleB;
    [SerializeField] private Toggle toggleAB;
    [SerializeField] private Toggle toggleRhNeg;
    [SerializeField] private Toggle toggleRhPos;
    [SerializeField] private Button getButton;
    private TubeSlot[] _tubeSlots;
    private bool AnyBloodTypeSelected => toggle0.isOn || toggleA.isOn || toggleB.isOn || toggleAB.isOn;
    private bool AnyRhSelected => toggleRhNeg.isOn || toggleRhPos.isOn;

    private void Awake()
    {
        _tubeSlots = GetComponentsInChildren<TubeSlot>();
        getButton.onClick.AddListener(SpawnBloodSampleIfAvailable);
    }

    private void Update()
    {
        getButton.interactable = AnyBloodTypeSelected && AnyRhSelected;
    }

    public void SpawnBloodSampleIfAvailable()
    {
        foreach (var tubeSlot in _tubeSlots)
        {
            if (tubeSlot.BloodSample is not null) continue;

            tubeSlot.BloodSample = Instantiate(bloodSamplePrefab).GetComponent<BloodSample>();
            tubeSlot.BloodSample.BloodClass = GenerateBloodClass();
            //return true;
            return;
        }

        //return false;
    }

    private BloodClass GenerateBloodClass()
    {
        var allowedBloodTypes = new List<BloodType>();
        var allowedRh = new List<bool>();
        
        if (toggle0.isOn)
        {
            allowedBloodTypes.Add(BloodType.O);
        }
        if (toggleA.isOn)
        {
            allowedBloodTypes.Add(BloodType.A);
        }
        if (toggleB.isOn)
        {
            allowedBloodTypes.Add(BloodType.B);
        }
        if (toggleAB.isOn)
        {
            allowedBloodTypes.Add(BloodType.AB);
        }
        if (toggleRhNeg.isOn)
        {
            allowedRh.Add(false);
        }
        if (toggleRhPos.isOn)
        {
            allowedRh.Add(true);
        }

        return new BloodClass(allowedBloodTypes, allowedRh);
    }
}
