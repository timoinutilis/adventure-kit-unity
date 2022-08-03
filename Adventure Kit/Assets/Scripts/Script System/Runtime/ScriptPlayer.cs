using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer
{
    public readonly MonoBehaviour monoBehaviour;
    public readonly CommandManager commandManager;
    public readonly VariableManager variableManager;

    private AdventureScript adventureScript;
    private bool isLoopEnabled;

    private int startLineIndex = 0;
    private int lineIndex = 0;
    private readonly List<ICommandExecution> currentExecutions = new();
    private bool isAwaiting;
    private bool needsAwaitBeforeEnd;

    public bool IsRunning { get; private set; } = false;

    public ScriptPlayer(MonoBehaviour monoBehaviour, CommandManager commandManager, VariableManager variableManager)
    {
        this.monoBehaviour = monoBehaviour;
        this.commandManager = commandManager;
        this.variableManager = variableManager;
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
            try
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
                            needsAwaitBeforeEnd = true;
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
            catch (ScriptException exception)
            {
                Debug.LogError($"{exception.Message} at {adventureScript.sourceCode.name}:{lineIndex + 1}");
                StopExecution();
            }
        }
    }

    public ICommandExecution ExecuteScriptLine(ScriptLine scriptLine)
    {
        ICommand command = commandManager.GetCommand(scriptLine.GetArg(0));
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
        needsAwaitBeforeEnd = false;
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
        if (needsAwaitBeforeEnd)
        {
            throw new ScriptException("Missing 'Await' command after 'DoNotWait'");
        }

        lineIndex = startLineIndex;
        if (!isLoopEnabled)
        {
            IsRunning = false;
        }
    }
}
