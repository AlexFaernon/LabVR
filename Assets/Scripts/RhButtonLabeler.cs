using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RhButtonLabeler : MonoBehaviour
{
    [SerializeField] private bool rh;
    private TubeLabeler _tubeLabeler;
    private TMP_Text _buttonLabel;

    private void Awake()
    {
        _tubeLabeler = GetComponentInParent<TubeLabeler>();
        _buttonLabel = GetComponentInChildren<TMP_Text>();
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(_ => _tubeLabeler.labelRh = rh);
    }

    private void Update()
    {
        _buttonLabel.color = _tubeLabeler.labelRh == rh && _tubeLabeler.BloodSample is not null ? Color.green : Color.white;
    }
}
