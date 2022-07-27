using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLocationCommand : ICommand
{
    public string Name
    {
        get { return "ChangeLocation"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        VariableManager vm = scriptPlayer.variableManager;

        string sceneName = scriptLine.GetArgValue(1, vm);
        string positionName = scriptLine.GetArgValue(2, vm);

        LocationManager.Instance.ChangeLocation(sceneName, positionName);
        return null;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        string sceneName = scriptLine.GetArgValue(1, null);
        _ = scriptLine.GetArgValue(2, null);
        scriptLine.ExpectEndOfLine(3);

        int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (buildIndex == -1)
        {
            throw new ScriptException($"Did not find scene with name '{sceneName}'");
        }
    }
}
