using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureScript : MonoBehaviour
{
    public TextAsset sourceCode;

    private CommandManager commandManager;
    private ScriptLine[] scriptLines;
    private Dictionary<string, int> labelLineIndices = new();

    public ScriptLine[] ScriptLines => scriptLines;

    public void Prepare()
    {
        if (scriptLines != null)
        {
            return;
        }

        commandManager = GlobalScriptPlayer.Instance.commandManager;

        Parse();
        Test();
    }

    public int GetLineIndexForLabel(string label)
    {
        if (!labelLineIndices.ContainsKey(label))
        {
            throw new ScriptException($"Undefined label '{label}'");
        }
        return labelLineIndices[label];
    }

    private void Parse()
    {
        string[] sourceLines = sourceCode.text.Split("\n");
        scriptLines = new ScriptLine[sourceLines.Length];
        for (int i = 0; i < sourceLines.Length; i++)
        {
            ScriptLine line = new(sourceLines[i], i + 1);
            scriptLines[i] = line;
            if (line.Label != null)
            {
                labelLineIndices[line.Label] = i;
            }
        }
    }

    private void Test()
    {
        for (int lineIndex = 0; lineIndex < ScriptLines.Length; ++lineIndex)
        {
            try
            {
                ScriptLine scriptLine = ScriptLines[lineIndex];
                if (!scriptLine.IsEmpty && scriptLine.Label == null)
                {
                    TestScriptLine(scriptLine);
                }
            }
            catch (ScriptException exception)
            {
                Debug.LogError($"{exception.Message} at {sourceCode.name}:{lineIndex + 1}");
            }
        }
    }

    public void TestScriptLine(ScriptLine scriptLine)
    {
        ICommand command = commandManager.GetCommand(scriptLine.GetArg(0));
        command.Test(this, scriptLine);
    }
}
