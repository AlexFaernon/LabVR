using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletHole : MonoBehaviour
{
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip stirringSound;
    private readonly HashSet<BloodClass> _plasma = new();
    private readonly HashSet<BloodClass> _formedElements = new();
    public HashSet<DropperContent> Content { get; } = new();
    private bool _readyToAgglutinate;
    public bool IsStirred => _stirringDistance > 0.15f;
    public bool IsAgglutinated { get; private set; }
    private float _stirringDistance;
    private Vector3 _lastStirringRodPos;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_readyToAgglutinate && IsStirred)
        {
            IsAgglutinated = true;
        }
    }
    
    public void Fill(DropperContent newContent, BloodClass bloodClass = null)
    {
        if (newContent is DropperContent.Plasma or DropperContent.FormedElements && bloodClass is null)
        {
            throw new ArgumentException("Blood without type");
        }
        
        Debug.Log($"added {newContent}");
        _audioSource.PlayOneShot(dropSound);
        
        _stirringDistance = 0;
        switch (newContent)
        {
            case DropperContent.None:
                break;
            case DropperContent.Plasma:
                Content.Add(newContent);
                _plasma.Add(bloodClass);
                _readyToAgglutinate = _readyToAgglutinate || AgglutinationChecker.PlasmaAndErythrocyte(_plasma, Content);
                break;
            case DropperContent.FormedElements:
                Content.Add(newContent);
                _formedElements.Add(bloodClass);
                _readyToAgglutinate = _readyToAgglutinate || AgglutinationChecker.FormedElementsAndAntiGenes(_formedElements, Content);
                break;
            case DropperContent.AntiA:
            case DropperContent.AntiB:
            case DropperContent.AntiD:
                Content.Add(newContent);
                _readyToAgglutinate = _readyToAgglutinate || AgglutinationChecker.FormedElementsAndAntiGenes(_formedElements, Content);
                break;
            case DropperContent.ErythrocyteA:
            case DropperContent.ErythrocyteB:
            case DropperContent.Erythrocyte0:
                Content.Add(newContent);
                _readyToAgglutinate = _readyToAgglutinate || AgglutinationChecker.PlasmaAndErythrocyte(_plasma, Content);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Debug.Log($"Agglutination: {_readyToAgglutinate}");
    }

    public void Clear()
    {
        _plasma.Clear();
        _formedElements.Clear();
        Content.Clear();
        _readyToAgglutinate = false;
        IsAgglutinated = false;
        _stirringDistance = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Stirring Rod") || Content.Count == 0 || IsStirred || IsAgglutinated) return;

        _lastStirringRodPos = other.transform.position;
        _audioSource.clip = stirringSound;
        _audioSource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Stirring Rod")) return;

        var stirringRodPos = other.transform.position;
        _stirringDistance += (stirringRodPos - _lastStirringRodPos).magnitude;
        _lastStirringRodPos = stirringRodPos;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Stirring Rod"))
        {
            _audioSource.Stop();
        }
    }
}
