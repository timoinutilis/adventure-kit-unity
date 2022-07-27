using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCommand : ICommand
{
    public string Name
    {
        get { return "Take"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        VariableManager vm = scriptPlayer.variableManager;

        string itemName = scriptLine.GetArgValue(1, vm);

        InventoryItem item = Resources.Load<InventoryItem>("InventoryItems/" + itemName);
        Inventory.Instance.Add(item);
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
