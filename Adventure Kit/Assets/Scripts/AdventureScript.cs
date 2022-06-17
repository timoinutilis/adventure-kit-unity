using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureScript : MonoBehaviour
{
    public TextAsset sourceCode;

    private ScriptLine[] scriptLines;
    private Dictionary<string, int> labelLineIndices = new();

    public ScriptLine[] ScriptLines
    {
        get => scriptLines;
    }

    public void Prepare()
    {
        if (scriptLines != null)
        {
            return;
        }

        string[] sourceLines = sourceCode.text.Split("\n");
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

    // File.WriteAllText(AssetDatabase.GetAssetPath(TEXT_ASSET), STRING);
    // EditorUtility.SetDirty(TEXT_ASSET);
}
