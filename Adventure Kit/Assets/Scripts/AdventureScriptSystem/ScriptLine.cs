using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;

public class ScriptLine
{
    private string[] args;

    public int LineNumber { get; }
    public string Label { get; }
    public bool IsEmpty => args.Length == 0;

    public ScriptLine(string sourceLine, int lineNumber)
    {
        LineNumber = lineNumber;

        MatchCollection matches = Regex.Matches(sourceLine, @"--.*|"".*?""|\S+");
        int count = matches.Count;

        // ignore comment
        if (count > 0 && matches[count - 1].Value.StartsWith("--"))
        {
            count -= 1;
        }

        // copy arguments
        args = new string[count];
        for (int i = 0; i < count; i++)
        {
            string value = matches[i].Value;
            if (value.StartsWith('"'))
            {
                value = value.Substring(1, value.Length - 2);
            }
            args[i] = value;
        }

        // check for label
        if (args.Length == 1 && args[0].EndsWith(':'))
        {
            Label = args[0].Substring(0, args[0].Length - 1);
        }
    }

    // Copy script line removing the first arguments
    public ScriptLine(ScriptLine sourceScriptLine, int startArgIndex)
    {
        if (startArgIndex >= sourceScriptLine.args.Length)
        {
            throw new ScriptException("Unexpected end of line");
        }
        LineNumber = sourceScriptLine.LineNumber;
        int len = sourceScriptLine.args.Length - startArgIndex;
        args = new string[len];
        Array.Copy(sourceScriptLine.args, startArgIndex, args, 0, len);
    }


    // literal argument
    public string GetArg(int index)
    {
        if (index >= args.Length)
        {
            throw new ScriptException("Missing parameter(s)");
        }
        return args[index];
    }

    // argument or value of its variable
    public string GetArgValue(int index)
    {
        string arg = GetArg(index);
        if (arg.StartsWith("$"))
        {
            return GlobalScriptPlayer.Instance.variableManager.GetValueForKey(arg[1..]);
        }
        return arg;
    }

    // argument value as int
    public int GetArgInt(int index)
    {
        string value = GetArgValue(index);
        try
        {
            return int.Parse(value);
        }
        catch (FormatException)
        {
            throw new ScriptException($"Expected integer number instead of '{value}'");
        }
    }

    // argument value as float
    public float GetArgFloat(int index)
    {
        string value = GetArgValue(index);
        try
        {
            return float.Parse(value, CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            throw new ScriptException($"Expected number instead of '{value}'");
        }
    }

    // GameObject with name of argument value
    public GameObject GetArgGameObject(int index)
    {
        string value = GetArgValue(index);
        GameObject obj = GameObject.Find(value);
        if (obj == null)
        {
            throw new ScriptException($"Cannot find object with name '{value}'");
        }
        return obj;
    }

    // literal argument with label validation
    public string GetArgLabel(int index, ScriptPlayer scriptPlayer = null)
    {
        string arg = GetArg(index);
        if (arg.StartsWith("$"))
        {
            throw new ScriptException("Label cannot be a variable");
        }
        scriptPlayer?.CheckLabel(arg);
        return arg;
    }
    
    public void ExpectKeyword(int index, string keyword)
    {
        if (index >= args.Length || args[index] != keyword)
        {
            throw new ScriptException($"Missing keyword '{keyword}'");
        }
    }

    public bool HasKeyword(string keyword)
    {
        foreach (var arg in args)
        {
            if (arg == keyword) return true;
        }
        return false;
    }

    public bool HasDoNotWait()
    {
        return HasKeyword("DoNotWait");
    }
}
