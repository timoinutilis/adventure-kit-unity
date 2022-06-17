using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCommand : ICommand
{
    public string Name
    {
        get { return "Take"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        string itemName = scriptLine.GetArgValue(1);
        InventoryItem item = Resources.Load<InventoryItem>("InventoryItems/" + itemName);
        Inventory.Instance.Add(item);
        return true;
    }
}
