using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class InteractionEditorUtils
{
    public static void ScriptLabelProperty(SerializedProperty label, AdventureScript adventureScript, string defaultLabel)
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(label);

        if (label.stringValue.Length == 0)
        {
            var gameObject = (label.serializedObject.targetObject as MonoBehaviour).gameObject;
            if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
            {
                label.stringValue = defaultLabel;
                GUI.FocusControl(null);
                adventureScript.EditScript(label.stringValue, true);
            }
        }
        else
        {
            if (GUILayout.Button("Edit", GUILayout.ExpandWidth(false)))
            {
                adventureScript.EditScript(label.stringValue, false);
            }
        }
        GUILayout.EndHorizontal();
    }
}
