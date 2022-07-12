using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name { get; }
    bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine);
}
