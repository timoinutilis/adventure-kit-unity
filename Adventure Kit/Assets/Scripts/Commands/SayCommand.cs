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
        VariableManager vm = scriptPlayer.variableManager;

        GameObject gameObject = scriptLine.GetArgGameObject(1, vm);
        string text = scriptLine.GetArgValue(2, vm);
        bool doNotWait = scriptLine.HasDoNotWait();

        ActorController actor = gameObject.GetComponent<ActorController>();

        SayCommandExecution execution = new();
        execution.actor = actor;
        execution.WaitForEnd = !doNotWait;

        actor.Say(text, () => scriptPlayer.Continue(execution));

        return execution;
    }

#if DEBUG
    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        _ = scriptLine.GetArgGameObject(1, null);
        _ = scriptLine.GetArgValue(2, null);
        // DoNotWait
        scriptLine.ExpectEndOfLine(4);
    }
#endif

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
