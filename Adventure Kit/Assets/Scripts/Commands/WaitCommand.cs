using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : ICommand
{
    public string Name
    {
        get { return "Wait"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        float seconds = scriptLine.GetArgFloat(1);
        scriptPlayer.StartCoroutine(WaitCoroutine(scriptPlayer, seconds));
        return false;
    }

    IEnumerator WaitCoroutine(ScriptPlayer scriptPlayer, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scriptPlayer.Continue();
    }
}
