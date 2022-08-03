using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScriptPlayer : MonoBehaviour
{
    public static GlobalScriptPlayer Instance { get; private set; }

    public CommandManager commandManager;
    public VariableManager variableManager;

    private ScriptPlayer scriptPlayer;

    public bool IsRunning => scriptPlayer.IsRunning;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new UnityException("GlobalScriptPlayer must exist only once");
        }
        Instance = this;

        scriptPlayer = new ScriptPlayer(this, commandManager, variableManager);
    }

    public void Execute(AdventureScript adventureScript, string startLabel)
    {
        scriptPlayer.Execute(adventureScript, startLabel, false);
    }

    public void StopExecution()
    {
        scriptPlayer.StopExecution();
    }
}
