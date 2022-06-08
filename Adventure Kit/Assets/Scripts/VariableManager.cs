using System.Collections;
using System.Collections.Generic;

public class VariableManager
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
