using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScriptPlayer : MonoBehaviour
{
    public AdventureScript adventureScript;
    public string startLabel;
    public bool startsImmediately;
    public bool isLoopEnabled;

    private ScriptPlayer scriptPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GlobalScriptPlayer global = GlobalScriptPlayer.Instance;
        scriptPlayer = new ScriptPlayer(this, global.commandManager, global.variableManager);

        if (startsImmediately)
        {
            Execute();
        }
    }

    public void Execute()
    {
        scriptPlayer.Execute(adventureScript, startLabel, isLoopEnabled);
    }

    public void StopExecution()
    {
        scriptPlayer.StopExecution();
    }
}
