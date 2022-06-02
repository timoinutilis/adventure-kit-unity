using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    public string Name
    {
        get { return "Jump"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, string[] args)
    {
        scriptPlayer.JumpToLabel(args[1]);
        return true;
    }
}
