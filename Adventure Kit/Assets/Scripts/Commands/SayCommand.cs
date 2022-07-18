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
        bool doNotWait = scriptLine.HasDoNotWait();

        ActorController actor = gameObject.GetComponent<ActorController>();

        SayCommandExecution execution = new();
        execution.actor = actor;
        execution.WaitForEnd = !doNotWait;

        actor.Say(text, () => scriptPlayer.Continue(execution));

        return execution;
    }


    private class SayCommandExecution : ICommandExecution
    {
        public ActorController actor;

        public bool WaitForEnd { get; set; }

        public void Cancel(ScriptPlayer scriptPlayer)
        {
            actor.Cancel();
        }
    }
}
