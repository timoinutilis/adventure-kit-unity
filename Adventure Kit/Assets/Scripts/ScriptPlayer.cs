using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    public AdventureScript adventureScript;
    public bool startsImmediately;
    public bool isLoopEnabled;

    private int lineIndex = 0;
    private bool isRunning = false;

    public int LineIndex
    {
        get;
        set;
    }

    public bool IsRunning
    {
        get;
    }

    private readonly ICommand[] availableCommands =
    {
        new IfCommand(),
        new JumpCommand(),
        new LetCommand(),
        new SayCommand(),
        new WaitCommand(),
        new WalkCommand()
    };

    private readonly Dictionary<string, ICommand> commandsByName = new();

    void Awake()
    {
        foreach (ICommand command in availableCommands)
        {
            commandsByName[command.Name] = command;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startsImmediately)
        {
            isRunning = true;
            Execute();
        }
    }

    public void Execute()
    {
        while (isRunning)
        {
            ScriptLine line = adventureScript.ScriptLines[lineIndex];
            ICommand command = commandsByName[line.Args[0]];

            bool finished = command.Execute(this, line.Args);
            if (finished)
            {
                Next();
            }
            else
            {
                return;
            }
        }
    }

    public void Continue()
    {
        Next();
        Execute();
    }

    public void JumpToLabel(string label)
    {
        lineIndex = adventureScript.GetLineIndexForLabel(label);
    }

    private void Next()
    {
        lineIndex++;
        if (lineIndex >= adventureScript.ScriptLines.Length)
        {
            if (isLoopEnabled)
            {
                lineIndex = 0;
            }
            else
            {
                isRunning = false;
            }
        }
    }

}
