using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    void OnEnable()
    {
        priority = serializedObject.FindProperty("priority");
        adventureScript = serializedObject.FindProperty("adventureScript");
        combinations = serializedObject.FindProperty("combinations");
        fallbackCombinations = serializedObject.FindProperty("fallbackCombinations");
        fallbackCombinationLabel = serializedObject.FindProperty("fallbackCombinationLabel");
        interactions = serializedObject.FindProperty("interactions");
        fallbackInteractionLabel = serializedObject.FindProperty("fallbackInteractionLabel");

        CombinationDrawer.onClickEditEvent.AddListener(onClickEditCombination);
        DualCombinationDrawer.onClickEditEvent.AddListener(onClickEditCombination);
    }

    private void OnDisable()
    {
        CombinationDrawer.onClickEditEvent.RemoveListener(onClickEditCombination);
        DualCombinationDrawer.onClickEditEvent.RemoveListener(onClickEditCombination);
    }


    public override void OnInspectorGUI()
    {
        InventoryItemHandler inventoryItemHandler = (InventoryItemHandler)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(priority);
        EditorGUILayout.PropertyField(adventureScript);
        EditorGUILayout.PropertyField(combinations);
        EditorGUILayout.PropertyField(fallbackCombinations);
        InteractionEditorUtils.ScriptLabelProperty(fallbackCombinationLabel, inventoryItemHandler.adventureScript, "OnCombineFallback");
        EditorGUILayout.PropertyField(interactions);
        InteractionEditorUtils.ScriptLabelProperty(fallbackInteractionLabel, inventoryItemHandler.adventureScript, "OnInteractFallback");

        serializedObject.ApplyModifiedProperties();
    }

    void onClickEditCombination(MonoBehaviour monoBehaviour, string label, bool createIfNew)
    {
        InventoryItemHandler inventoryItemHandler = (InventoryItemHandler)target;

        if (monoBehaviour == inventoryItemHandler)
        {
            inventoryItemHandler.adventureScript.EditScript(label, createIfNew);
        }
    }
}
