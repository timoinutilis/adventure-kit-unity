using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptPlayer))]
public class ScriptPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScriptPlayer scriptPlayer = (ScriptPlayer)target;
        if (scriptPlayer.adventureScript == null)
        {
            if (GUILayout.Button("Create Script"))
            {
                scriptPlayer.adventureScript = ScriptableObject.CreateInstance<AdventureScript>();
            }
        }
    }
}
