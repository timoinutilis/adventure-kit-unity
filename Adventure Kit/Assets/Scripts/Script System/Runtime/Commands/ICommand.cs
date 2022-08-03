using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name { get; }
    ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine);
#if DEBUG
    void Test(AdventureScript adventureScript, ScriptLine scriptLine);
#endif
}

public interface ICommandExecution
{
    bool WaitForEnd { get; }
    void Cancel(ScriptPlayer scriptPlayer);
}
