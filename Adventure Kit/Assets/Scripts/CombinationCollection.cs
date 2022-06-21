using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CombinationCollection
{
    public Combination[] combinations;
    public string fallbackLabel;

    public string FindLabel(InventoryItem item)
    {
        foreach (var combination in combinations)
        {
            if (combination.item == item)
            {
                return combination.label;
            }
        }
        if (!String.IsNullOrEmpty(fallbackLabel))
        {
            return fallbackLabel;
        }
        return null;
    }
}
