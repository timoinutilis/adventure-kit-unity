using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : SaveGameContent
{
    [Serializable]
    class VariableManagerData
    {
        public List<Variable> variables;
    }

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

    public override string Key()
    {
        return "VariableManager";
    }

    public override void Reset()
    {
        variables.Clear();
    }

    public override string ToJson()
    {
        VariableManagerData data = new();
        data.variables = variables;
        return JsonUtility.ToJson(data);
    }

    public override void FromJson(string json)
    {
        var data = JsonUtility.FromJson<VariableManagerData>(json);
        variables = data.variables;
    }
}

[Serializable]
public class Variable
{
    public string key;
    public string value;
}