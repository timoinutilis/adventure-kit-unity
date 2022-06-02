using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureScript : MonoBehaviour
{
    [TextArea(5, 20)]
    public string sourceCode;

    private ScriptLine[] scriptLines;
    private Dictionary<string, int> labelLineIndices = new();

    public ScriptLine[] ScriptLines
    {
        get => scriptLines;
    }

    void Awake()
    {
        string[] sourceLines = sourceCode.Split("\n");
        scriptLines = new ScriptLine[sourceLines.Length];
        for (int i = 0; i < sourceLines.Length; i++)
        {
            ScriptLine line = new ScriptLine(sourceLines[i]);
            scriptLines[i] = line;
            if (line.IsLabel)
            {
                labelLineIndices[line.Args[0]] = i;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLineIndexForLabel(string label)
    {
        return labelLineIndices[label];
    }
}
