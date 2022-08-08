using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(InventoryItemHandler))]
public class InventoryItemHandlerEditor : Editor
{
    SerializedProperty priority;
    SerializedProperty adventureScript;
    SerializedProperty combinations;
    SerializedProperty fallbackCombinations;
    SerializedProperty fallbackCombinationLabel;
    SerializedProperty interactions;
    SerializedProperty fallbackInteractionLabel;

    private ReorderableList combinationsList;
    private ReorderableList fallbackCombinationsList;
    private ReorderableList interactionsList;

    void OnEnable()
    {
        priority = serializedObject.FindProperty("priority");
        adventureScript = serializedObject.FindProperty("adventureScript");
        combinations = serializedObject.FindProperty("combinations");
        fallbackCombinations = serializedObject.FindProperty("fallbackCombinations");
        fallbackCombinationLabel = serializedObject.FindProperty("fallbackCombinationLabel");
        interactions = serializedObject.FindProperty("interactions");
        fallbackInteractionLabel = serializedObject.FindProperty("fallbackInteractionLabel");

        combinationsList = new ReorderableList(serializedObject, combinations, true, false, true, true)
        {
            drawElementCallback = DrawCombination,
            elementHeightCallback = DualCombinationEditorUtils.ElementHeight,
            onAddCallback = DualCombinationEditorUtils.AddElement
        };
        
        fallbackCombinationsList = new ReorderableList(serializedObject, fallbackCombinations, true, false, true, true)
        {
            drawElementCallback = DrawFallbackCombination,
            elementHeightCallback = CombinationEditorUtils.ElementHeight,
            onAddCallback = CombinationEditorUtils.AddElement
        };

        interactionsList = new ReorderableList(serializedObject, interactions, true, false, true, true)
        {
            drawElementCallback = DrawInteraction,
            elementHeightCallback = CombinationEditorUtils.ElementHeight,
            onAddCallback = CombinationEditorUtils.AddElement
        };
    }

    public override void OnInspectorGUI()
    {
        InventoryItemHandler inventoryItemHandler = (InventoryItemHandler)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(priority);
        EditorGUILayout.PropertyField(adventureScript);

        combinations.isExpanded = EditorGUILayout.Foldout(combinations.isExpanded, "Combinations", true);
        if (combinations.isExpanded)
        {
            combinationsList.DoLayoutList();
        }

        fallbackCombinations.isExpanded = EditorGUILayout.Foldout(fallbackCombinations.isExpanded, "Fallback Combinations", true);
        if (fallbackCombinations.isExpanded)
        {
            fallbackCombinationsList.DoLayoutList();
        }

        InteractionEditorUtils.ScriptLabelProperty(fallbackCombinationLabel, inventoryItemHandler.adventureScript, "OnCombineFallback");

        interactions.isExpanded = EditorGUILayout.Foldout(interactions.isExpanded, "Interactions", true);
        if (interactions.isExpanded)
        {
            interactionsList.DoLayoutList();
        }

        InteractionEditorUtils.ScriptLabelProperty(fallbackInteractionLabel, inventoryItemHandler.adventureScript, "OnInteractFallback");

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCombination(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty property = combinationsList.serializedProperty.GetArrayElementAtIndex(index);
        InventoryItemHandler inventoryItemHandler = (InventoryItemHandler)target;
        DualCombinationEditorUtils.DrawProperty(rect, property, inventoryItemHandler.adventureScript, CombinationDefaultLabel);
    }

    private string CombinationDefaultLabel(SerializedProperty property)
    {
        InventoryItem inventoryItem1 = property.FindPropertyRelative("item1").objectReferenceValue as InventoryItem;
        InventoryItem inventoryItem2 = property.FindPropertyRelative("item2").objectReferenceValue as InventoryItem;
        if (inventoryItem1 == null || inventoryItem2 == null)
        {
            return null;
        }
        return $"OnUse{inventoryItem1.name}With{inventoryItem2.name}";
    }
    
    private void DrawFallbackCombination(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty property = fallbackCombinationsList.serializedProperty.GetArrayElementAtIndex(index);
        InventoryItemHandler inventoryItemHandler = (InventoryItemHandler)target;
        CombinationEditorUtils.DrawProperty(rect, property, inventoryItemHandler.adventureScript, FallbackCombinationDefaultLabel);
    }

    private string FallbackCombinationDefaultLabel(SerializedProperty property)
    {
        SerializedProperty itemProperty = property.FindPropertyRelative("item");
        InventoryItem inventoryItem = itemProperty.objectReferenceValue as InventoryItem;
        if (inventoryItem == null)
        {
            return null;
        }
        return $"OnUse{inventoryItem.name}Fallback";
    }

    private void DrawInteraction(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty property = interactionsList.serializedProperty.GetArrayElementAtIndex(index);
        InventoryItemHandler inventoryItemHandler = (InventoryItemHandler)target;
        CombinationEditorUtils.DrawProperty(rect, property, inventoryItemHandler.adventureScript, InteractionDefaultLabel);
    }

    private string InteractionDefaultLabel(SerializedProperty property)
    {
        SerializedProperty itemProperty = property.FindPropertyRelative("item");
        InventoryItem inventoryItem = itemProperty.objectReferenceValue as InventoryItem;
        if (inventoryItem == null)
        {
            return null;
        }
        return $"OnClick{inventoryItem.name}";
    }
}
