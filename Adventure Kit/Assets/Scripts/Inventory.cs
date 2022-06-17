using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> items = new();

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
    }

    public void Remove(InventoryItem item)
    {
        items.Remove(item);
    }
}
