using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetCommand : ICommand
{
    public string Name
    {
        get { return "Let"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        string key = scriptLine.GetArg(1);
        string value = scriptLine.GetArgValue(3);
        ScriptPlayerSettings settings = ScriptPlayerSettings.Instance;
        settings.variableManager.SetValueForKey(key, value);
        return true;
    }
}
