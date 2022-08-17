using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandManager : CommandManager
{
    public LocationManager locationManager;
    public Inventory inventory;
    public ChoiceManager choiceManager;

    protected override void AddCustomCommands()
    {
        AddCommand(new SayCommand());
        AddCommand(new WalkCommand());
        AddCommand(new ChoiceCommand(choiceManager));
        AddCommand(new ShowChoicesCommand(choiceManager));
        AddCommand(new TakeCommand(inventory));
        AddCommand(new DropCommand(inventory));
        AddCommand(new ChangeLocationCommand(locationManager));
    }
}
