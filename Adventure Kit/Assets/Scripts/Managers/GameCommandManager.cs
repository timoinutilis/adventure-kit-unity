using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandManager : CommandManager
{
    protected override void AddCustomCommands()
    {
        AddCommand(new SayCommand());
        AddCommand(new WalkCommand());
        AddCommand(new ChoiceCommand());
        AddCommand(new ShowChoicesCommand());
        AddCommand(new TakeCommand());
        AddCommand(new DropCommand());
        AddCommand(new ChangeLocationCommand());
    }
}
