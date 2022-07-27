using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetCommand : ICommand
{
    public string Name
    {
        get { return "Let"; }
    }
    
    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        VariableManager vm = scriptPlayer.variableManager;

        string key = scriptLine.GetArgVariable(1);
        string value = scriptLine.GetArgValue(3, vm);

        scriptPlayer.variableManager.SetValueForKey(key, value);
        return null;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        _ = scriptLine.GetArgVariable(1);
        scriptLine.ExpectKeyword(2, "=");
        _ = scriptLine.GetArgValue(3, null);
        scriptLine.ExpectEndOfLine(4);
    }
}
