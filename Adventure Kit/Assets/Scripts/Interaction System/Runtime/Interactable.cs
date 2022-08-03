using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AdventureScript adventureScript;
    public string clickLabel;

    [DefaultLabel("OnUse{item}With{object}")]
    public Combination[] combinations;

    public string fallbackCombinationLabel;

    public void OnInteract()
    {
        GlobalScriptPlayer.Instance.Execute(adventureScript, clickLabel);
    }

    public void OnCombineWithItem(InventoryItem item)
    {
        foreach (var combination in combinations)
        {
            if (combination.item == item)
            {
                GlobalScriptPlayer.Instance.Execute(adventureScript, combination.label);
                return;
            }
        }
        if (!String.IsNullOrEmpty(fallbackCombinationLabel))
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, fallbackCombinationLabel);
        }
        else
        {
            Inventory.Instance.OnCombine(item, null);
        }
    }
}
