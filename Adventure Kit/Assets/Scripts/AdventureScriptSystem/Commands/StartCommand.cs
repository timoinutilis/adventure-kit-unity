using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCommand : ICommand
{
    public string Name
    {
        get { return "Start"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        GameObject gameObject = scriptLine.GetArgGameObject(1);
        LocalScriptPlayer objectScriptPlayer = gameObject.GetComponent<LocalScriptPlayer>();
        objectScriptPlayer.Execute();
        return true;
    }
}
