using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChoicesCommand : ICommand
{
    public string Name
    {
        get { return "ShowChoices"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        ShowChoicesCommandExecution execution = new();
        ChoiceManager.Instance.Show(() => scriptPlayer.Continue(execution));
        return execution;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        scriptLine.ExpectEndOfLine(1);
    }

    private class ShowChoicesCommandExecution : ICommandExecution
    {
        public bool WaitForEnd => true;

        public void Cancel(ScriptPlayer scriptPlayer)
        {
            ChoiceManager.Instance.Clear();
        }
    }
}