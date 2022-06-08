using System.Collections;
using System.Collections.Generic;

public class ScriptPlayerSettings
{
    private static ScriptPlayerSettings _instance;

    public static ScriptPlayerSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScriptPlayerSettings();
            }
            return _instance;
        }
    }


    public VariableManager variableManager;

    private readonly Dictionary<string, ICommand> commandsByName = new();

    public ScriptPlayerSettings()
    {
        // add built in commands
        AddCommand(new IfCommand());
        AddCommand(new JumpCommand());
        AddCommand(new LetCommand());
        AddCommand(new WaitCommand());
    }

    public void AddCommand(ICommand command)
    {
        commandsByName[command.Name] = command;
    }

    public ICommand GetCommand(string name)
    {
        return commandsByName[name];
    }
}
