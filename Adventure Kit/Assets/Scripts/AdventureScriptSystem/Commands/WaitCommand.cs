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

        execution.seconds = seconds;
        execution.coroutine = WaitCoroutine(scriptPlayer, execution);
        scriptPlayer.monoBehaviour.StartCoroutine(execution.coroutine);
        return execution;
    }

    IEnumerator WaitCoroutine(ScriptPlayer scriptPlayer, WaitCommandExecution execution)
    {
        yield return new WaitForSeconds(execution.seconds);
        scriptPlayer.Continue(execution);
    }


    private class WaitCommandExecution : ICommandExecution
    {
        public float seconds;
        public IEnumerator coroutine;

        public bool WaitForEnd => true;

        public void Cancel(ScriptPlayer scriptPlayer)
        {
            scriptPlayer.monoBehaviour.StopCoroutine(coroutine);
        }
    }
}
