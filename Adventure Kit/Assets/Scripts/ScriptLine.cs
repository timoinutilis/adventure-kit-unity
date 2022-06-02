using System.Collections;
using System.Text.RegularExpressions;

public class ScriptLine
{
    private string[] args;
    private string label;

    public string[] Args
    {
        get => args;
    }

    public string Label
    {
        get => label;
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
}
