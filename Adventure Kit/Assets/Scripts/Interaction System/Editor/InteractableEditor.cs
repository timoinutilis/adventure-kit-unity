using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor
{
    private SerializedProperty adventureScript;
    private SerializedProperty clickLabel;
    private SerializedProperty combinations;
    private SerializedProperty fallbackCombinationLabel;

    private ReorderableList combinationsList;

    private void OnEnable()
    {
        adventureScript = serializedObject.FindProperty("adventureScript");
        clickLabel = serializedObject.FindProperty("clickLabel");
        combinations = serializedObject.FindProperty("combinations");
        fallbackCombinationLabel = serializedObject.FindProperty("fallbackCombinationLabel");

        combinationsList = new ReorderableList(serializedObject, combinations, true, false, true, true)
        {
            drawElementCallback = DrawElement,
            elementHeightCallback = CombinationEditorUtils.ElementHeight
        };
    }

    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(adventureScript);
        InteractionEditorUtils.ScriptLabelProperty(clickLabel, interactable.adventureScript, $"OnClick{interactable.gameObject.name}");

        combinations.isExpanded = EditorGUILayout.Foldout(combinations.isExpanded, "Combinations", true);
        if (combinations.isExpanded)
        {
            combinationsList.DoLayoutList();
        }

        InteractionEditorUtils.ScriptLabelProperty(fallbackCombinationLabel, interactable.adventureScript, $"OnFallback{interactable.gameObject.name}");

        serializedObject.ApplyModifiedProperties();
    }
    
    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty property = combinationsList.serializedProperty.GetArrayElementAtIndex(index);
        Interactable interactable = (Interactable)target;
        CombinationEditorUtils.DrawProperty(rect, property, interactable.adventureScript, DefaultLabel);
    }

    private string DefaultLabel(SerializedProperty property)
    {
        SerializedProperty itemProperty = property.FindPropertyRelative("item");
        InventoryItem inventoryItem = itemProperty.objectReferenceValue as InventoryItem;
        if (inventoryItem == null)
        {
            return null;
        }
        Interactable interactable = (Interactable)target;
        return $"OnUse{inventoryItem.name}With{interactable.gameObject.name}";
    }
}
