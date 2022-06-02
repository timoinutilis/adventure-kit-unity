using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCommand : ICommand
{
    public string Name
    {
        get { return "Walk"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, string[] args)
    {
        GameObject actionObject = GameObject.Find(args[1]);
        GameObject targetObject = GameObject.Find(args[2]);

        actionObject.transform.position = targetObject.transform.position;

        return true;
    }
}
