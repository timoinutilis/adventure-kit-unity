using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCommand : ICommand
{
    public string Name
    {
        get { return "Walk"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        GameObject actionObject = scriptLine.GetArgGameObject(1);
        GameObject targetObject = scriptLine.GetArgGameObject(2);

        actionObject.transform.position = targetObject.transform.position;

        return true;
    }
}
