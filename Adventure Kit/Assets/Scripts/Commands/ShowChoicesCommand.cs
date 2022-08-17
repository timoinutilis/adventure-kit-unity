using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChoicesCommand : ICommand
{
    private ChoiceManager choiceManager;

    public ShowChoicesCommand(ChoiceManager choiceManager)
    {
        this.choiceManager = choiceManager;
    }

    public string Name
    {
        get { return "ShowChoices"; }
    }

    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        ShowChoicesCommandExecution execution = new();
        execution.choiceManager = choiceManager;
        choiceManager.Show(() => scriptPlayer.Continue(execution));
        return execution;
    }

#if DEBUG
    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        scriptLine.ExpectEndOfLine(1);
    }
#endif

    private class ShowChoicesCommandExecution : ICommandExecution
    {
        public ChoiceManager choiceManager;
        public bool WaitForEnd => true;

        public void Cancel(ScriptPlayer scriptPlayer)
        {
            choiceManager.Clear();
        }
    }
}