using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCommand : ICommand
{
    private Inventory inventory;

    public DropCommand(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public string Name
    {
        get { return "Drop"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        VariableManager vm = scriptPlayer.variableManager;

        string itemName = scriptLine.GetArgValue(1, vm);

        InventoryItem item = Resources.Load<InventoryItem>("InventoryItems/" + itemName);
        inventory.Remove(item);
        return null;
    }

#if DEBUG
    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        string itemName = scriptLine.GetArgValue(1, null);
        scriptLine.ExpectEndOfLine(2);

        InventoryItem item = Resources.Load<InventoryItem>("InventoryItems/" + itemName);
        if (item == null)
        {
            throw new ScriptException($"Undefined inventory item '{itemName}'");
        }
    }
#endif
}
