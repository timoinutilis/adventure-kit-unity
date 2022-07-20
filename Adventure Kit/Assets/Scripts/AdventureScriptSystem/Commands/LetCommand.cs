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
        string key = scriptLine.GetArg(1);
        scriptLine.ExpectKeyword(2, "=");
        string value = scriptLine.GetArgValue(3);
        if (!key.StartsWith("$"))
        {
            throw new ScriptException("Variable must start with $");
        }
        GlobalScriptPlayer.Instance.variableManager.SetValueForKey(key[1..], value);
        return null;
    }
}
