using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCommand : ICommand
{
    public string Name
    {
        get { return "Drop"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        string itemName = scriptLine.GetArgValue(1);
        InventoryItem item = Resources.Load<InventoryItem>("InventoryItems/" + itemName);
        Inventory.Instance.Remove(item);
        return null;
    }
}
