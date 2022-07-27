using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitCommand : ICommand
{
    public string Name => "Await";

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        scriptPlayer.SetIsAwaiting();
        return null;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        scriptLine.ExpectEndOfLine(1);
    }
}
