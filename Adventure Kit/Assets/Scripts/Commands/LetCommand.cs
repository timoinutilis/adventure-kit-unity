using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetCommand : ICommand
{
    public string Name
    {
        get { return "Let"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, string[] args)
    {
        return true;
    }
}
