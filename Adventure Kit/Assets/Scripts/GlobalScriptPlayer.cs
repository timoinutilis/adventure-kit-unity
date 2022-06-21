using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScriptPlayer : MonoBehaviour
{
    public static GlobalScriptPlayer Instance { get; private set; }

    public VariableManager variableManager;

    private ScriptPlayer scriptPlayer;
    private Dictionary<string, ICommand> commandsByName;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new UnityException("GlobalScriptPlayer must exist only once");
        }
        Instance = this;

        scriptPlayer = new ScriptPlayer(this);
        commandsByName = new();

        // add built in commands
        AddCommand(new IfCommand());
        AddCommand(new JumpCommand());
        AddCommand(new LetCommand());
        AddCommand(new WaitCommand());
        AddCommand(new SayCommand());
        AddCommand(new WalkCommand());
        AddCommand(new StartCommand());
        AddCommand(new ChoiceCommand());
        AddCommand(new ShowChoicesCommand());
        AddCommand(new TakeCommand());
        AddCommand(new DropCommand());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCommand(ICommand command)
    {
        commandsByName[command.Name] = command;
    }

    public ICommand GetCommand(string name)
    {
        return commandsByName[name];
    }

    public void Execute(AdventureScript adventureScript, string startLabel)
    {
        scriptPlayer.Execute(adventureScript, startLabel, false);
    }
}
