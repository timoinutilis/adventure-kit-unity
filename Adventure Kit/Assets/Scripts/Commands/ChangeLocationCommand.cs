using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocationCommand : ICommand
{
    public string Name
    {
        get { return "ChangeLocation"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        string sceneName = scriptLine.GetArgValue(1);
        LocationManager.Instance.ChangeLocation(sceneName);
        return true;
    }
}
