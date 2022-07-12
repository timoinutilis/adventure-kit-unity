using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> items = new();

    public readonly UnityEvent onChangeEvent = new();
    public readonly UnityEvent onDragChangeEvent = new();

    private InventoryItem draggingItem;
    private List<InventoryItemHandler> handlers = new();

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

    public void RemoveAll()
    {
        items.Clear();
        onChangeEvent.Invoke();
    }
    
    public void AddHandler(InventoryItemHandler handler)
    {
        handlers.Add(handler);
        handlers.Sort((x, y) => y.priority.CompareTo(x.priority));
    }

    public void RemoveHandler(InventoryItemHandler handler)
    {
        handlers.Remove(handler);
    }
    
    public void OnItemInteract(InventoryItem item)
    {
        foreach (var handler in handlers)
        {
            if (handler.Interact(item))
            {
                return;
            }
        }
    }

    public void OnCombine(InventoryItem item1, InventoryItem item2)
    {
        foreach (var handler in handlers)
        {
            if (handler.Combine(item1, item2))
            {
                return;
            }
        }
    }
}
