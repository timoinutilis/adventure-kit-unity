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
    private readonly List<ICommandExecution> currentExecutions = new();
    private bool isAwaiting;

    public bool IsRunning { get; private set; } = false;

    public ScriptPlayer(MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
    }

    public void Execute(AdventureScript adventureScript, string startLabel, bool isLoopEnabled)
    {
        if (IsRunning)
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
        IsRunning = true;
        ExecuteScriptLines();
    }

    public void StopExecution()
    {
        if (IsRunning)
        {
            foreach (var execution in currentExecutions)
            {
                execution.Cancel(this);
            }
            currentExecutions.Clear();
            IsRunning = false;
        }
    }

    private void ExecuteScriptLines()
    {
        while (IsRunning)
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
                ICommandExecution execution = ExecuteScriptLine(line);
                if (execution != null)
                {
                    currentExecutions.Add(execution);
                    if (execution.WaitForEnd)
                    {
                        return;
                    }
                    else
                    {
                        Next();
                    }
                }
                else if (isAwaiting)
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

    public void Continue(ICommandExecution execution)
    {
        if (!IsRunning)
        {
            throw new UnityException("Not running");
        }

        bool removed = currentExecutions.Remove(execution);
        if (!removed)
        {
            throw new UnityException("Command not executing");
        }

        if (execution.WaitForEnd)
        {
            Next();
            ExecuteScriptLines();
        }
        else if (isAwaiting && currentExecutions.Count == 0)
        {
            isAwaiting = false;
            Next();
            ExecuteScriptLines();
        }
    }

    public void JumpToLabel(string label)
    {
        lineIndex = adventureScript.GetLineIndexForLabel(label);
    }

    public void SetIsAwaiting()
    {
        if (currentExecutions.Count > 0)
        {
            isAwaiting = true;
        }
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
        if (currentExecutions.Count > 0)
        {
            throw new UnityException("Still executing commands on end, missing Await command");
        }

        lineIndex = startLineIndex;
        if (!isLoopEnabled)
        {
            IsRunning = false;
        }
    }
}
