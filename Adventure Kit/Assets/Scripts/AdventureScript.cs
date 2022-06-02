using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AdventureScript : ScriptableObject
{
    [TextArea(5, 20)]
    public string sourceCode;

    private ScriptLine[] scriptLines;
    private Dictionary<string, int> labelLineIndices = new();

    public ScriptLine[] ScriptLines
    {
        get => scriptLines;
    }

    public void Prepare()
    {
        string[] sourceLines = sourceCode.Split("\n");
        scriptLines = new ScriptLine[sourceLines.Length];
        for (int i = 0; i < sourceLines.Length; i++)
        {
            ScriptLine line = new ScriptLine(sourceLines[i]);
            scriptLines[i] = line;
            if (line.Label != null)
            {
                labelLineIndices[line.Label] = i;
            }
        }
    }

    public int GetLineIndexForLabel(string label)
    {
        return labelLineIndices[label];
    }
}
