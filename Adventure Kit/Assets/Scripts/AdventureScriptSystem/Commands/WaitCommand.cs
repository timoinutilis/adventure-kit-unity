using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : ICommand
{
    public string Name
    {
        get { return "Wait"; }
    }
    
    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        float seconds = scriptLine.GetArgFloat(1);

        WaitCommandExecution execution = new();

        execution.coroutine = WaitCoroutine(scriptPlayer, seconds);
        scriptPlayer.monoBehaviour.StartCoroutine(execution.coroutine);
        return execution;
    }

    IEnumerator WaitCoroutine(ScriptPlayer scriptPlayer, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scriptPlayer.Continue();
    }


    private class WaitCommandExecution : ICommandExecution
    {
        public IEnumerator coroutine;

        public void Cancel(ScriptPlayer scriptPlayer)
        {
            scriptPlayer.monoBehaviour.StopCoroutine(coroutine);
        }
    }
}
