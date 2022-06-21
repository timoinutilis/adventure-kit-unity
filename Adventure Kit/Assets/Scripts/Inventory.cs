using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> items = new();
    public AdventureScript adventureScript;
    public string fallbackInteractionLabel;
    public string fallbackCombinationLabel;

    public readonly UnityEvent onChangeEvent = new();
    public readonly UnityEvent onDragChangeEvent = new();

    private InventoryItem draggingItem;

    public InventoryItem DraggingItem
    {
        get => draggingItem;

        set
        {
            draggingItem = value;
            onDragChangeEvent.Invoke();
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            throw new UnityException("GlobalScriptPlayer must exist only once");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(InventoryItem item)
    {
        items.Add(item);
        onChangeEvent.Invoke();
    }

    public void Remove(InventoryItem item)
    {
        items.Remove(item);
        onChangeEvent.Invoke();
    }

    public void OnItemInteract(InventoryItem item)
    {
        if (!String.IsNullOrEmpty(item.defaultInteractionLabel))
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, item.defaultInteractionLabel);
        }
        else if (!String.IsNullOrEmpty(fallbackInteractionLabel))
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, fallbackInteractionLabel);
        }
    }

    public void OnCombine(InventoryItem item1, InventoryItem item2)
    {
        string label = item1.combinationCollection.FindLabel(item2) ?? item2.combinationCollection.FindLabel(item1);
        if (label != null)
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, label);
        }
        else if (!String.IsNullOrEmpty(fallbackCombinationLabel))
        {
            GlobalScriptPlayer.Instance.Execute(adventureScript, fallbackCombinationLabel);
        }
    }
}
