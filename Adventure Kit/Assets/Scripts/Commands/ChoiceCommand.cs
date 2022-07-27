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
        VariableManager vm = scriptPlayer.variableManager;

        string text = scriptLine.GetArgValue(1, vm);
        string label = scriptLine.GetArgValue(2, vm);

        ChoiceManager.Instance.AddChoice(text, () => scriptPlayer.JumpToLabel(label));
        return null;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        _ = scriptLine.GetArgValue(1, null);
        string label = scriptLine.GetArgValue(2, null);
        scriptLine.ExpectEndOfLine(3);

        _ = adventureScript.GetLineIndexForLabel(label);
    }
}
