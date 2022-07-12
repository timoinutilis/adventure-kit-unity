using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemHandler : MonoBehaviour
{
    public int priority;
    public AdventureScript adventureScript;
    public DualCombination[] combinations;
    public Combination[] fallbackCombinations;
    public string fallbackCombinationLabel;
    public Combination[] interactions;
    public string fallbackInteractionLabel;

    void Start()
    {
        Inventory.Instance.AddHandler(this);
    }

    void OnDestroy()
    {
        Inventory.Instance.RemoveHandler(this);
    }

    public bool Interact(InventoryItem item)
    {
        foreach (var combination in interactions)
        {
            if (combination.item == item)
            {
                GlobalScriptPlayer.Instance.Execute(adventureScript, combination.label);
                return true;
            }
        }
        if (!String.IsNullOrEmpty(fallbackInteractionLabel))
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, fallbackInteractionLabel);
            return true;
        }
        return false;
    }

    public bool Combine(InventoryItem item1, InventoryItem item2)
    {
        if (item2 != null)
        {
            foreach (var combination in combinations)
            {
                if ((combination.item1 == item1 && combination.item2 == item2)
                    || (combination.item1 == item2 && combination.item2 == item1))
                {
                    GlobalScriptPlayer.Instance.Execute(adventureScript, combination.label);
                    return true;
                }
            }
        }
        foreach (var combination in fallbackCombinations)
        {
            if (combination.item == item1 || combination.item == item2)
            {
                GlobalScriptPlayer.Instance.Execute(adventureScript, combination.label);
                return true;
            }
        }
        if (!String.IsNullOrEmpty(fallbackCombinationLabel))
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, fallbackCombinationLabel);
            return true;
        }
        return false;
    }
}
