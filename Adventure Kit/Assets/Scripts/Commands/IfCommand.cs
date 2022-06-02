using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : ICommand
{
    public string Name
    {
        get { return "If"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, string[] args)
    {
        return true;
    }
}
