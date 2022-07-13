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
        ChoiceManager.Instance.Show(scriptPlayer);
        return new ShowChoicesCommandExecution();
    }


    private class ShowChoicesCommandExecution : ICommandExecution
    {
        public void Cancel(ScriptPlayer scriptPlayer)
        {
            ChoiceManager.Instance.Clear();
        }
    }
}