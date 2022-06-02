using System.Collections;
using System.Text.RegularExpressions;

public class ScriptLine
{
    private string[] args;
    private bool isLabel;

    public string[] Args
    {
        get => args;
    }

    public bool IsLabel
    {
        get => isLabel;
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
            isLabel = true;
        }
    }
}
