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
            StartExecution();
        }
    }

    public void StartExecution()
    {
        if (isRunning)
        {
            Debug.Log("Already running");
            return;
        }
        isRunning = true;
        Execute();
    }

    private void Execute()
    {
        while (isRunning)
        {
            ScriptLine line = adventureScript.ScriptLines[lineIndex];
            if (line.Label != null)
            {
                // labels end the script
                End();
            }
            else if (line.IsEmpty)
            {
                // skip empty line
                Next();
            }
            else
            {
                // execute command
                bool finished = ExecuteScriptLine(line);
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

    public bool ExecuteScriptLine(ScriptLine scriptLine)
    {
        ScriptPlayerSettings settings = ScriptPlayerSettings.Instance;
        ICommand command = settings.GetCommand(scriptLine.GetArg(0));
        return command.Execute(this, scriptLine);
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
        lineIndex = startLineIndex;
        if (!isLoopEnabled)
        {
            isRunning = false;
        }
    }
}
