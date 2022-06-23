using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform location;
    public AdventureScript adventureScript;
    public string clickLabel;
    public Combination[] combinations;
    public string fallbackCombinationLabel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
