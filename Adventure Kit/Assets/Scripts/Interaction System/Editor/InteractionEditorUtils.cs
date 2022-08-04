using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class InteractionEditorUtils
{
    public static void ScriptLabelProperty(SerializedProperty property, AdventureScript adventureScript, string defaultLabel)
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(property);

        if (property.stringValue.Length == 0)
        {
            var gameObject = (property.serializedObject.targetObject as MonoBehaviour).gameObject;
            if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
            {
                property.stringValue = defaultLabel;
                GUI.FocusControl(null);
                adventureScript.EditScript(property.stringValue, true);
            }
        }
        else
        {
            if (GUILayout.Button("Edit", GUILayout.ExpandWidth(false)))
            {
                adventureScript.EditScript(property.stringValue, false);
            }
        }
        GUILayout.EndHorizontal();
    }
}
