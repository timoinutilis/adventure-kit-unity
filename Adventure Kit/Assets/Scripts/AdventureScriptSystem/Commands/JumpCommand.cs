using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    public string Name
    {
        get { return "Jump"; }
    }
    
    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        string label = scriptLine.GetArgValue(1);
        scriptPlayer.JumpToLabel(label);
        return null;
    }
}
