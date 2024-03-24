using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BloodTypeButtonLabeler : MonoBehaviour
{
    [SerializeField] private BloodType bloodType;
    private TubeLabeler _tubeLabeler;
    private TMP_Text _buttonLabel;

    private void Awake()
    {
        _tubeLabeler = GetComponentInParent<TubeLabeler>();
        _buttonLabel = GetComponentInChildren<TMP_Text>();
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(_ => _tubeLabeler.labelBloodType = bloodType);
    }

    private void Update()
    {
        _buttonLabel.color = _tubeLabeler.labelBloodType == bloodType ? Color.green : Color.white;
    }
}
