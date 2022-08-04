using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class DualCombinationEditorUtils
{
    public delegate string DefaultLabelDelegate(SerializedProperty property);

    public static float ElementHeight(int index)
    {
        return EditorGUIUtility.singleLineHeight * 3 + 2;
    }

    public static void DrawProperty(Rect rect, SerializedProperty property, AdventureScript adventureScript, DefaultLabelDelegate defaultLabelDelegate)
    {
        SerializedProperty item1Property = property.FindPropertyRelative("item1");
        SerializedProperty item2Property = property.FindPropertyRelative("item2");
        SerializedProperty labelProperty = property.FindPropertyRelative("label");

        var lineHeight = EditorGUIUtility.singleLineHeight;
        var item1Rect = new Rect(rect.x, rect.y, rect.width, lineHeight);
        var item2Rect = new Rect(rect.x, rect.y + lineHeight, rect.width, lineHeight);
        var labelRect = new Rect(rect.x, rect.y + lineHeight * 2, rect.width - 50, lineHeight);
        var buttonRect = new Rect(labelRect.x + labelRect.width, labelRect.y, 50, labelRect.height);

        EditorGUI.PropertyField(item1Rect, item1Property);
        EditorGUI.PropertyField(item2Rect, item2Property);
        EditorGUI.PropertyField(labelRect, labelProperty);

        if (labelProperty.stringValue.Length == 0)
        {
            if (GUI.Button(buttonRect, "Create"))
            {
                GUI.FocusControl(null);
                string label = defaultLabelDelegate(property);
                if (label == null)
                {
                    EditorUtility.DisplayDialog("No Item(s) Selected", "Please select both items before you create the label.", "OK");
                }
                else
                {
                    labelProperty.stringValue = label;
                    adventureScript.EditScript(labelProperty.stringValue, true);
                }
            }
        }
        else
        {
            if (GUI.Button(buttonRect, "Edit"))
            {
                adventureScript.EditScript(labelProperty.stringValue, false);
            }
        }
    }
}
