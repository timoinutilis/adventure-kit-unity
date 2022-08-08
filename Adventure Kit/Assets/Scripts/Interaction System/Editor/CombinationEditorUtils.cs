using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public static class CombinationEditorUtils
{
    public delegate string DefaultLabelDelegate(SerializedProperty property);

    public static float ElementHeight(int index)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 2;
    }

    public static void DrawProperty(Rect rect, SerializedProperty property, AdventureScript adventureScript, DefaultLabelDelegate defaultLabelDelegate)
    {
        SerializedProperty itemProperty = property.FindPropertyRelative("item");
        SerializedProperty labelProperty = property.FindPropertyRelative("label");

        var lineHeight = EditorGUIUtility.singleLineHeight;
        var itemRect = new Rect(rect.x, rect.y, rect.width, lineHeight);
        var labelRect = new Rect(rect.x, rect.y + lineHeight, rect.width - 50, lineHeight);
        var buttonRect = new Rect(labelRect.x + labelRect.width, labelRect.y, 50, labelRect.height);

        EditorGUI.PropertyField(itemRect, itemProperty);
        EditorGUI.PropertyField(labelRect, labelProperty);

        if (labelProperty.stringValue.Length == 0)
        {
            if (GUI.Button(buttonRect, "Create"))
            {
                GUI.FocusControl(null);
                string label = defaultLabelDelegate(property);
                if (label == null)
                {
                    EditorUtility.DisplayDialog("No Item Selected", "Please select the item before you create the label.", "OK");
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

    public static void AddElement(ReorderableList list)
    {
        list.serializedProperty.arraySize = list.count + 1;
        list.index = list.serializedProperty.arraySize - 1;
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(list.index);
        element.FindPropertyRelative("item").objectReferenceValue = null;
        element.FindPropertyRelative("label").stringValue = "";
    }
}
