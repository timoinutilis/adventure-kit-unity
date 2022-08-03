using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomPropertyDrawer(typeof(Combination))]
public class CombinationDrawer : PropertyDrawer
{
    // MonoBehaviour, label, createIfNew
    public static UnityEvent<MonoBehaviour, string, bool> onClickEditEvent = new();

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty itemProperty = property.FindPropertyRelative("item");
        SerializedProperty labelProperty = property.FindPropertyRelative("label");

        var lineHeight = EditorGUIUtility.singleLineHeight;
        var itemRect = new Rect(position.x, position.y, position.width, lineHeight);
        var labelRect = new Rect(position.x, position.y + lineHeight, position.width - 50, lineHeight);
        var buttonRect = new Rect(labelRect.x + labelRect.width, labelRect.y, 50, labelRect.height);

        EditorGUI.PropertyField(itemRect, itemProperty);
        EditorGUI.PropertyField(labelRect, labelProperty);

        if (labelProperty.stringValue.Length == 0)
        {
            if (GUI.Button(buttonRect, "Create"))
            {
                GUI.FocusControl(null);
                InventoryItem inventoryItem = itemProperty.objectReferenceValue as InventoryItem;
                if (inventoryItem == null)
                {
                    EditorUtility.DisplayDialog("No Item Selected", "Please select an item before you create the label.", "OK");
                }
                else
                {
                    MonoBehaviour monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
                    labelProperty.stringValue = $"OnUse{inventoryItem.name}With{monoBehaviour.gameObject.name}";
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
