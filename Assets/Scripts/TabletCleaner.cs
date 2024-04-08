using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCleaner : MonoBehaviour
{
    private TabletHole[] _tabletHoles;

    private void Awake()
    {
        _tabletHoles = GetComponentsInChildren<TabletHole>();
    }

    public void ClearTablet()
    {
        foreach (var tabletHole in _tabletHoles)
        {
            tabletHole.Clear();
        }
    }
}
