using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandManager : MonoBehaviour
{
    private readonly Dictionary<string, ICommand> commandsByName = new();

    private void Awake()
    {
        // add built in commands
        AddCommand(new IfCommand());
        AddCommand(new JumpCommand());
        AddCommand(new LetCommand());
        AddCommand(new StartCommand());
        AddCommand(new WaitCommand());

        AddCustomCommands();
    }

    protected abstract void AddCustomCommands();

    protected void AddCommand(ICommand command)
    {
        commandsByName[command.Name] = command;
    }

    public ICommand GetCommand(string name)
    {
        return commandsByName[name];
    }
}
