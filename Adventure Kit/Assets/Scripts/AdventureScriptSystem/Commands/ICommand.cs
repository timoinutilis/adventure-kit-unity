using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name { get; }
    ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine);
}

public interface ICommandExecution
{
    void Cancel(ScriptPlayer scriptPlayer);
}
