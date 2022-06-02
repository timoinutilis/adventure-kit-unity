using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICommand
{
    string Name { get; }
    bool Execute(ScriptPlayer scriptPlayer, string[] args);
}
