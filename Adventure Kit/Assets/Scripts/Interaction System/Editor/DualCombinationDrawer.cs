using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomPropertyDrawer(typeof(DualCombination))]
public class DualCombinationDrawer : PropertyDrawer
{
    // MonoBehaviour, label, createIfNew
    public static UnityEvent<MonoBehaviour, string, bool> onClickEditEvent = new();

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 3 + 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty item1Property = property.FindPropertyRelative("item1");
        SerializedProperty item2Property = property.FindPropertyRelative("item2");
        SerializedProperty labelProperty = property.FindPropertyRelative("label");

        var lineHeight = EditorGUIUtility.singleLineHeight;
        var item1Rect = new Rect(position.x, position.y, position.width, lineHeight);
        var item2Rect = new Rect(position.x, position.y + lineHeight, position.width, lineHeight);
        var labelRect = new Rect(position.x, position.y + lineHeight * 2, position.width - 50, lineHeight);
        var buttonRect = new Rect(labelRect.x + labelRect.width, labelRect.y, 50, labelRect.height);

        EditorGUI.PropertyField(item1Rect, item1Property);
        EditorGUI.PropertyField(item2Rect, item2Property);
        EditorGUI.PropertyField(labelRect, labelProperty);

        if (labelProperty.stringValue.Length == 0)
        {
            if (GUI.Button(buttonRect, "Create"))
            {
                GUI.FocusControl(null);
                var inventoryItem1 = item1Property.objectReferenceValue as InventoryItem;
                var inventoryItem2 = item2Property.objectReferenceValue as InventoryItem;
                if (inventoryItem1 == null || inventoryItem2 == null)
                {
                    EditorUtility.DisplayDialog("No Items Selected", "Please select both items before you create the label.", "OK");
                }
                else
                {
                    MonoBehaviour monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
                    labelProperty.stringValue = $"OnUse{inventoryItem1.name}With{inventoryItem2.name}";
                    onClickEditEvent.Invoke(monoBehaviour, labelProperty.stringValue, true);
                }
            }
        }
        else
        {
            if (GUI.Button(buttonRect, "Edit"))
            {
                MonoBehaviour monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
                onClickEditEvent.Invoke(monoBehaviour, labelProperty.stringValue, false);
            }
        }

        EditorGUI.EndProperty();
    }
}
