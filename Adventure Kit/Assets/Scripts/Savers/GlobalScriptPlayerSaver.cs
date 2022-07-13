using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class GlobalScriptPlayerSaver : Saver
{
    public GlobalScriptPlayer globalScriptPlayer;

    public override string SaveGameKey => null;

    public override void FromSaveGameObject(JObject obj)
    {
    }

    public override JObject ToSaveGameObject()
    {
        return null;
    }

    public override void Reset()
    {
        globalScriptPlayer.StopExecution();
    }

}
