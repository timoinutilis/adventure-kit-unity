using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class VariableManager : SaveGameContent
{
    public List<Variable> variables = new();

    public string GetValueForKey(string key)
    {
        Variable variable = FindVariable(key);
        if (variable != null)
        {
            return variable.value;
        }
        return null;
    }

    public void SetValueForKey(string key, string value)
    {
        Variable variable = FindVariable(key);
        if (variable == null)
        {
            variable = new();
            variable.key = key;
            variables.Add(variable);
        }

        variable.value = value;
    }

    private Variable FindVariable(string key)
    {
        foreach (var variable in variables)
        {
            if (variable.key == key)
            {
                return variable;
            }
        }
        return null;
    }

    // SaveGameContent

    class VariableManagerData
    {
        public Dictionary<string, string> Variables;
    }

    public override string SaveGameKey()
    {
        return "VariableManager";
    }

    public override void Reset()
    {
        variables.Clear();
    }

    public override JObject ToSaveGameObject()
    {
        VariableManagerData data = new()
        {
            Variables = variables.ToDictionary(v => v.key, v => v.value)
        };
        return JObject.FromObject(data);
    }

    public override void FromSaveGameObject(JObject obj)
    {
        variables.Clear();

        VariableManagerData data = obj.ToObject<VariableManagerData>();

        foreach (var variable in data.Variables)
        {
            SetValueForKey(variable.Key, variable.Value);
        }
    }
}

[Serializable]
public class Variable
{
    public string key;
    public string value;
}