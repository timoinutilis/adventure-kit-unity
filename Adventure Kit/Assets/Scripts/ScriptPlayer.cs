using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    public AdventureScript adventureScript;
    public bool startsImmediately;
    public bool isLoopEnabled;
    public string startLabel;

    private int startLineIndex = 0;
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
        adventureScript.Prepare();

        if (startLabel != null && startLabel.Length > 0)
        {
            startLineIndex = adventureScript.GetLineIndexForLabel(startLabel);
            if (startLineIndex + 1 < adventureScript.ScriptLines.Length)
            {
                // skip line of label
                startLineIndex++;
            }
            lineIndex = startLineIndex;
        }

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
            if (line.Label != null)
            {
                // labels end the script
                End();
            }
            else if (line.Args.Length == 0)
            {
                // skip empty line
                Next();
            }
            else
            {
                // execute command
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
            End();
        }
    }

    private void End()
    {
        if (isLoopEnabled)
        {
            lineIndex = startLineIndex;
        }
        else
        {
            isRunning = false;
        }
    }

}
