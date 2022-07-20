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
        string value1 = scriptLine.GetArgValue(1);
        string comparator = scriptLine.GetArg(2);
        string value2 = scriptLine.GetArgValue(3);
        scriptLine.ExpectKeyword(4, "Then");

        if (Validate(value1, comparator, value2))
        {
            ScriptLine thenScriptLine = new ScriptLine(scriptLine, 5);
            return scriptPlayer.ExecuteScriptLine(thenScriptLine);
        }

        return null;
    }

    bool Validate(string value1, string comparator, string value2)
    {
        switch (comparator)
        {
            case "=":
                return value1 == value2;
            case "<>":
                return value1 != value2;
            case ">":
                return int.Parse(value1) > int.Parse(value2);
            case "<":
                return int.Parse(value1) < int.Parse(value2);
            case ">=":
                return int.Parse(value1) >= int.Parse(value2);
            case "<=":
                return int.Parse(value1) <= int.Parse(value2);
            default:
                throw new ScriptException($"Unknown comparator '{comparator}'");
        }
    }
}
