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
        VariableManager vm = scriptPlayer.variableManager;

        GameObject gameObject = scriptLine.GetArgGameObject(1, vm);

        LocalScriptPlayer objectScriptPlayer = gameObject.GetComponent<LocalScriptPlayer>();
        objectScriptPlayer.Execute();
        return null;
    }

#if DEBUG
    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        GameObject gameObject = scriptLine.GetArgGameObject(1, null);
        scriptLine.ExpectEndOfLine(2);

        LocalScriptPlayer objectScriptPlayer = gameObject.GetComponent<LocalScriptPlayer>();
        if (objectScriptPlayer == null)
        {
            throw new ScriptException($"Object '{gameObject.name}' does not have a LocalScriptPlayer");
        }
    }
#endif
}
