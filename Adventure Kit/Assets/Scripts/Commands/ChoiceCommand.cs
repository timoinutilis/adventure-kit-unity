using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCommand : ICommand
{
    public string Name
    {
        get { return "Choice"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        string text = scriptLine.GetArgValue(1);
        string label = scriptLine.GetArgLabel(2, scriptPlayer);

        ChoiceManager.Instance.AddChoice(text, () => scriptPlayer.JumpToLabel(label));
        return null;
    }
}
