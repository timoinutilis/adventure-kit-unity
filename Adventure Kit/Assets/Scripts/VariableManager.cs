using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    private readonly Dictionary<string, string> variables = new();

    public string GetValueForKey(string key)
    {
        if (variables.ContainsKey(key))
        {
            return variables[key];
        }
        return null;
    }

    public void SetValueForKey(string key, string value)
    {
        variables[key] = value;
    }
}
