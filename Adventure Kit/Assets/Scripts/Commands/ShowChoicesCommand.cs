using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChoicesCommand : ICommand
{
    public string Name
    {
        get { return "ShowChoices"; }
    }

    public bool Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        ChoiceManager.Instance.Show(scriptPlayer);
        return false;
    }
}