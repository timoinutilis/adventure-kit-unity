using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayCommand : ICommand
{
    public string Name
    {
        get { return "Say"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, string[] args)
    {
        GameObject actionObject = GameObject.Find(args[1]);
        Debug.Log(args[2]);
        return true;
    }
}
