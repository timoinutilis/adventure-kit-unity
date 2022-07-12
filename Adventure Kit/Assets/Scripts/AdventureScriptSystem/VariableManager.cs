using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
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
}
