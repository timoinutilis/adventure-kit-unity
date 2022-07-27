using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : ICommand
{
    public string Name
    {
        get { return "If"; }
    }
    
    public ICommandExecution Execute(ScriptPlayer scriptPlayer, ScriptLine scriptLine)
    {
        VariableManager vm = scriptPlayer.variableManager;

        string value1 = scriptLine.GetArgValue(1, vm);
        string comparator = scriptLine.GetArg(2);
        string value2 = scriptLine.GetArgValue(3, vm);

        if (Validate(value1, comparator, value2))
        {
            ScriptLine thenScriptLine = new(scriptLine, 5);
            return scriptPlayer.ExecuteScriptLine(thenScriptLine);
        }

        return null;
    }

    public void Test(AdventureScript adventureScript, ScriptLine scriptLine)
    {
        string value1 = scriptLine.GetArgValue(1, null);
        string comparator = scriptLine.GetArg(2);
        string value2 = scriptLine.GetArgValue(3, null);
        scriptLine.ExpectKeyword(4, "Then");
        Validate(value1, comparator, value2);
        ScriptLine thenScriptLine = new(scriptLine, 5);
        adventureScript.TestScriptLine(thenScriptLine);
    }

    bool Validate(string value1, string comparator, string value2)
    {
        try
        {
            switch (comparator)
            {
                case "=":
                    return value1 == value2;
                case "<>":
                    return value1 != value2;
                case ">":
                    return float.Parse(value1 ?? "0") > float.Parse(value2 ?? "0");
                case "<":
                    return float.Parse(value1 ?? "0") < float.Parse(value2 ?? "0");
                case ">=":
                    return float.Parse(value1 ?? "0") >= float.Parse(value2 ?? "0");
                case "<=":
                    return float.Parse(value1 ?? "0") <= float.Parse(value2 ?? "0");
                default:
                    throw new ScriptException($"Unknown comparator '{comparator}'");
            }
        }
        catch (FormatException)
        {
            throw new ScriptException($"Expected numbers for comparator '{comparator}'");
        }
    }
}
