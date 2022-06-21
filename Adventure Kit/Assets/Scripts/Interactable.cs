using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform location;
    public AdventureScript adventureScript;
    public string clickLabel;
    public CombinationCollection combinationCollection = new();
    //public Combination[] combinations;
    //public string fallbackCombinationLabel;

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
        var label = combinationCollection.FindLabel(item);
        if (label != null)
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, label);
        }
        //foreach (var combination in combinations)
        //{
        //    if (combination.item == item)
        //    {
        //        GlobalScriptPlayer.Instance.Execute(adventureScript, combination.label);
        //        return;
        //    }
        //}
        //if (fallbackCombinationLabel != null && fallbackCombinationLabel != "")
        //{
        //    GlobalScriptPlayer.Instance.Execute(adventureScript, fallbackCombinationLabel);
        //}
    }
}
