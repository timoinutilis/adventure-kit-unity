using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalScriptPlayer))]
public class LocalScriptPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LocalScriptPlayer scriptPlayer = (LocalScriptPlayer)target;
        if (scriptPlayer.adventureScript == null)
        {
            if (GUILayout.Button("Create Script"))
            {
                scriptPlayer.adventureScript = ScriptableObject.CreateInstance<AdventureScript>();
            }
        }
    }
}
