using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayCommand : ICommand
{
    public string Name
    {
        get { return "Say"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        GameObject gameObject = scriptLine.GetArgGameObject(1);
        string text = scriptLine.GetArgValue(2);

        ActorController actor = gameObject.GetComponent<ActorController>();
        actor.Say(text, scriptPlayer);
        return false;
    }
}
