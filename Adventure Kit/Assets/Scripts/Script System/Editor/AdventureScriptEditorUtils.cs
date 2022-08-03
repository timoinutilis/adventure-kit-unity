using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class AdventureScriptEditorUtils
{
    public static void EditScript(this AdventureScript adventureScript, string label, bool createIfNew)
    {
        int lineNumber = FindLabelLine(adventureScript.sourceCode.text, label);

        if (lineNumber == -1 && createIfNew)
        {
            string path = AssetDatabase.GetAssetPath(adventureScript.sourceCode);

            StreamWriter writer = new(path, true);
            writer.WriteLine("");
            writer.WriteLine(label + ":");
            writer.Close();

            AssetDatabase.ImportAsset(path);
            lineNumber = FindLabelLine(adventureScript.sourceCode.text, label);
        }

        AssetDatabase.OpenAsset(adventureScript.sourceCode, lineNumber);
    }

    public static int FindLabelLine(string text, string label)
    {
        string labelLine = label + ":";
        string[] lines = text.Split("\n");
        for (int i = 0; i < lines.Length; ++i)
        {
            if (lines[i].Contains(labelLine))
            {
                return i + 1;
            }
        }
        return -1;
    }
}
