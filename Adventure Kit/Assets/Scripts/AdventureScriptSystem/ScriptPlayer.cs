using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer
{
    public readonly MonoBehaviour monoBehaviour;

    private AdventureScript adventureScript;
    private bool isLoopEnabled;

    private int startLineIndex = 0;
    private int lineIndex = 0;
    private bool isRunning = false;
    private ICommandExecution currentExecution;

    public int LineIndex
    {
        get;
        set;
    }

    public bool IsRunning
    {
        get;
    }

    public ScriptPlayer(MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
    }

    public void Execute(AdventureScript adventureScript, string startLabel, bool isLoopEnabled)
    {
        if (isRunning)
        {
            throw new UnityException("Already running");
        }

        this.adventureScript = adventureScript;
        this.isLoopEnabled = isLoopEnabled;

        adventureScript.Prepare();

        if (startLabel != null && startLabel.Length > 0)
        {
            startLineIndex = adventureScript.GetLineIndexForLabel(startLabel);
            if (startLineIndex + 1 < adventureScript.ScriptLines.Length)
            {
                // skip line of label
                startLineIndex++;
            }
        }
        else
        {
            startLineIndex = 0;
        }

        lineIndex = startLineIndex;
        isRunning = true;
        ExecuteScriptLines();
    }

    public void StopExecution()
    {
        if (isRunning)
        {
            if (currentExecution != null)
            {
                currentExecution.Cancel(this);
                currentExecution = null;
            }
            isRunning = false;
        }
    }

    private void ExecuteScriptLines()
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
                currentExecution = ExecuteScriptLine(line);
                if (currentExecution != null)
                {
                    return;
                }
                else
                {
                    Next();
                }
            }
        }
    }

    public ICommandExecution ExecuteScriptLine(ScriptLine scriptLine)
    {
        ICommand command = GlobalScriptPlayer.Instance.commandManager.GetCommand(scriptLine.GetArg(0));
        return command.Execute(this, scriptLine);
    }

    public void Continue()
    {
        if (isRunning)
        {
            currentExecution = null;
            Next();
            ExecuteScriptLines();
        }
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
