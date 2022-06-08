using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayCommand : ICommand
{
    public string Name
    {
        get { return "Say"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        GameObject actionObject = scriptLine.GetArgGameObject(1);
        Debug.Log(scriptLine.GetArgValue(2));
        return true;
    }
}
