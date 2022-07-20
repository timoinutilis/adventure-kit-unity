using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCommand : ICommand
{
    public string Name
    {
        get { return "Start"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        GameObject gameObject = scriptLine.GetArgGameObject(1);
        LocalScriptPlayer objectScriptPlayer = gameObject.GetComponent<LocalScriptPlayer>();
        if (objectScriptPlayer == null)
        {
            throw new ScriptException($"Object '{gameObject.name}' does not have a LocalScriptPlayer");
        }
        objectScriptPlayer.Execute();
        return null;
    }
}
