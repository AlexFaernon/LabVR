using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletHole : MonoBehaviour
{
    private readonly HashSet<BloodClass> _plasma = new();
    private readonly HashSet<BloodClass> _formedElements = new();
    private readonly HashSet<DropperContent> _content = new();
    private bool _isAgglutinated;

    public void Fill(DropperContent newContent, BloodClass bloodClass = null)
    {
        if (newContent is DropperContent.Plasma or DropperContent.FormedElements && bloodClass is null)
        {
            throw new ArgumentException("Blood without type");
        }
        
        Debug.Log($"added {newContent}");
        switch (newContent)
        {
            case DropperContent.None:
                break;
            case DropperContent.Plasma:
                _plasma.Add(bloodClass);
                _isAgglutinated = _isAgglutinated || AgglutinationChecker.PlasmaAndErythrocyte(_plasma, _content);
                break;
            case DropperContent.FormedElements:
                _formedElements.Add(bloodClass);
                _isAgglutinated = _isAgglutinated || AgglutinationChecker.FormedElementsAndAntiGenes(_formedElements, _content);
                break;
            case DropperContent.AntiA:
            case DropperContent.AntiB:
            case DropperContent.AntiD:
                _content.Add(newContent);
                _isAgglutinated = _isAgglutinated || AgglutinationChecker.FormedElementsAndAntiGenes(_formedElements, _content);
                break;
            case DropperContent.ErythrocyteA:
            case DropperContent.ErythrocyteB:
            case DropperContent.Erythrocyte0:
                _content.Add(newContent);
                _isAgglutinated = _isAgglutinated || AgglutinationChecker.PlasmaAndErythrocyte(_plasma, _content);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Debug.Log($"Agglutination: {_isAgglutinated}");
    }
}
