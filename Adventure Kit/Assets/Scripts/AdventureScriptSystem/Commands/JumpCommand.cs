using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    public string Name
    {
        get { return "Jump"; }
    }
    
    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        VariableManager vm = scriptPlayer.variableManager;

        string label = scriptLine.GetArgValue(1, vm);

        scriptPlayer.JumpToLabel(label);
        return null;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        string label = scriptLine.GetArgValue(1, null);
        scriptLine.ExpectEndOfLine(2);

        _ = adventureScript.GetLineIndexForLabel(label);
    }
}
