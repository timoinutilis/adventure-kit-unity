using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;

public class ScriptLine
{
    private string[] args;
    private string label;

    public string Label
    {
        get => label;
    }

    public bool IsEmpty
    {
        get => args.Length == 0;
    }

    public ScriptLine(string sourceLine)
    {
        MatchCollection matches = Regex.Matches(sourceLine, @""".*?""|\S+");
        args = new string[matches.Count];
        for (int i = 0; i < matches.Count; i++)
        {
            string value = matches[i].Value;
            if (value.StartsWith('"'))
            {
                value = value.Substring(1, value.Length - 2);
            }
            args[i] = value;
        }
        if (args.Length == 1 && args[0].EndsWith(':'))
        {
            label = args[0].Substring(0, args[0].Length - 1);
        }
    }

    // Copy script line removing the first arguments
    public ScriptLine(ScriptLine sourceScriptLine, int startArgIndex)
    {
        int len = sourceScriptLine.args.Length - startArgIndex;
        args = new string[len];
        Array.Copy(sourceScriptLine.args, startArgIndex, args, 0, len);
    }


    // literal argument
    public string GetArg(int index)
    {
        return args[index];
    }

    // argument or value of its variable
    public string GetArgValue(int index)
    {
        string arg = GetArg(index);
        if (arg.StartsWith("$"))
        {
            ScriptPlayerSettings settings = ScriptPlayerSettings.Instance;
            return settings.variableManager.GetValueForKey(arg);
        }
        return arg;
    }

    // argument value as int
    public int GetArgInt(int index)
    {
        string value = GetArgValue(index);
        return int.Parse(value);
    }

    // argument value as float
    public float GetArgFloat(int index)
    {
        string value = GetArgValue(index);
        return float.Parse(value, CultureInfo.InvariantCulture);
    }

    // GameObject with name of argument value
    public GameObject GetArgGameObject(int index)
    {
        string value = GetArgValue(index);
        return GameObject.Find(value);
    }
}
