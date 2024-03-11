using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletHole : MonoBehaviour
{
    private readonly HashSet<BloodClass> _plasma = new();
    private readonly HashSet<BloodClass> _formedElements = new();
    private readonly HashSet<DropperContent> _content = new();

    public void Fill(DropperContent content, BloodClass bloodClass = null)
    {
        if (content is DropperContent.Plasma or DropperContent.FormedElements && bloodClass is null)
        {
            throw new ArgumentException("Blood without type");
        }
        
        Debug.Log($"added {content}");
        switch (content)
        {
            case DropperContent.None:
                break;
            case DropperContent.Plasma:
                _plasma.Add(bloodClass);
                break;
            case DropperContent.FormedElements:
                _formedElements.Add(bloodClass);
                break;
            case DropperContent.AntiA:
            case DropperContent.AntiB:
            case DropperContent.AntiD:
            case DropperContent.ErythrocyteA:
            case DropperContent.ErythrocyteB:
            case DropperContent.Erythrocyte0:
            default:
                _content.Add(content);
                break;
        }
    }
}
