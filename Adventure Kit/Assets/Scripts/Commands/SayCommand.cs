using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayCommand : ICommand
{
    public string Name
    {
        get { return "Say"; }
    }
    
    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        GameObject gameObject = scriptLine.GetArgGameObject(1);
        string text = scriptLine.GetArgValue(2);

        SayCommandExecution execution = new();

        execution.actor = gameObject.GetComponent<ActorController>();
        execution.actor.Say(text, scriptPlayer);
        return execution;
    }


    private class SayCommandExecution : ICommandExecution
    {
        public ActorController actor;

        public void Cancel(ScriptPlayer scriptPlayer)
        {
            actor.Cancel();
        }
    }
}
