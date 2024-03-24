using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AgglutinationChecker
{
    public static bool PlasmaAndErythrocyte(IEnumerable<BloodClass> plasma, HashSet<DropperContent> reagents)
    {
        var result = false;
        var containsA = reagents.Contains(DropperContent.ErythrocyteA);
        var containsB = reagents.Contains(DropperContent.ErythrocyteB);
        foreach (var bloodClass in plasma)
        {
            switch (bloodClass.BloodType)
            {
                case BloodType.O:
                    result = result || containsA || containsB;
                    break; 
                case BloodType.A:
                    result = result || containsB;
                    break; 
                case BloodType.B:
                    result = result || containsA;
                    break; 
                case BloodType.AB:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (result)
            {
                break;
            }
        }

        return result;
    }

    public static bool FormedElementsAndAntiGenes(IEnumerable<BloodClass> formedElements, HashSet<DropperContent> reagents)
    {
        var result = false;
        var containsAntiA = reagents.Contains(DropperContent.AntiA);
        var containsAntiB = reagents.Contains(DropperContent.AntiB);
        var containsAntiD = reagents.Contains(DropperContent.AntiD);

        foreach (var bloodClass in formedElements)
        {
            switch (bloodClass.BloodType)
            {
                case BloodType.O:
                    break;
                case BloodType.A:
                    result = result || containsAntiA;
                    break;
                case BloodType.B:
                    result = result || containsAntiB;
                    break;
                case BloodType.AB:
                    result = result || containsAntiA || containsAntiB;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            result = result || bloodClass.Rh & containsAntiD;

            if (result)
            {
                break;
            }
        }

        return result;
    }
}
