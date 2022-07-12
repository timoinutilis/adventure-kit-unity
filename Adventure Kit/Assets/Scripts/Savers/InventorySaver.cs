using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class InventorySaver : Saver
{
    public Inventory inventory;

    private class InventoryData
    {
        public List<string> ItemNames;
    }

    public override string SaveGameKey => "Inventory";

    public override JObject ToSaveGameObject()
    {
        InventoryData data = new()
        {
            ItemNames = inventory.items.ConvertAll(item => item.name)
        };
        return JObject.FromObject(data);
    }

    public override void FromSaveGameObject(JObject obj)
    {
        InventoryData data = obj.ToObject<InventoryData>();

        Reset();

        foreach (var itemName in data.ItemNames)
        {
            InventoryItem item = Resources.Load<InventoryItem>("InventoryItems/" + itemName);
            inventory.Add(item);
        }
    }

    public override void Reset()
    {
        inventory.RemoveAll();
        inventory.DraggingItem = null;
    }
}
