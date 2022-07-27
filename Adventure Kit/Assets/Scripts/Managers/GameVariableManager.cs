using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariableManager : VariableManager
{
    public List<GameVariable> variables = new();

    public override string GetValueForKey(string key)
    {
        GameVariable variable = FindVariable(key);
        if (variable != null)
        {
            return variable.value;
        }
        return "";
    }

    public override void SetValueForKey(string key, string value)
    {
        GameVariable variable = FindVariable(key);
        if (variable == null)
        {
            variable = new();
            variable.key = key;
            variables.Add(variable);
        }

        variable.value = value;
    }

    private GameVariable FindVariable(string key)
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
}

[Serializable]
public class GameVariable
{
    public string key;
    public string value;
}
