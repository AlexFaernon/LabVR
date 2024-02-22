using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSample : MonoBehaviour
{
    public BloodType BloodType { get; private set; }
    public bool Rh { get; private set; }
    [SerializeField] private GameObject notSeparatedContent;
    [SerializeField] private GameObject separatedContent;
    public bool isSeparated;

    private void Update()
    {
        notSeparatedContent.SetActive(!isSeparated);
        separatedContent.SetActive(isSeparated);
    }
}
