using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Linq;

public class GameVariableManagerSaver : Saver
{
    public GameVariableManager variableManager;

    private class VariableManagerData
    {
        public Dictionary<string, string> Variables;
    }

    public override string SaveGameKey => "VariableManager";

    public override void Reset()
    {
        variableManager.variables.Clear();
    }

    public override JObject ToSaveGameObject()
    {
        VariableManagerData data = new()
        {
            Variables = variableManager.variables.ToDictionary(v => v.key, v => v.value)
        };
        return JObject.FromObject(data);
    }

    public override void FromSaveGameObject(JObject obj)
    {
        variableManager.variables.Clear();

        VariableManagerData data = obj.ToObject<VariableManagerData>();

        foreach (var variable in data.Variables)
        {
            variableManager.SetValueForKey(variable.Key, variable.Value);
        }
    }
}
