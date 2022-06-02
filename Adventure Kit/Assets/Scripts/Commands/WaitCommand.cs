using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class WaitCommand : ICommand
{
    public string Name
    {
        get { return "Wait"; }
    }
    
    public bool Execute(ScriptPlayer scriptPlayer, string[] args)
    {
        float seconds = float.Parse(args[1], CultureInfo.InvariantCulture);
        scriptPlayer.StartCoroutine(ExampleCoroutine(scriptPlayer, seconds));
        return false;
    }

    IEnumerator ExampleCoroutine(ScriptPlayer scriptPlayer, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scriptPlayer.Continue();
    }
}
