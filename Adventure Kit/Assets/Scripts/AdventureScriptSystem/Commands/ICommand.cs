using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name { get; }
    ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine);
    void Test(AdventureScript adventureScript, ScriptLine scriptLine);
}

public interface ICommandExecution
{
    bool WaitForEnd { get; }
    void Cancel(ScriptPlayer scriptPlayer);
}
