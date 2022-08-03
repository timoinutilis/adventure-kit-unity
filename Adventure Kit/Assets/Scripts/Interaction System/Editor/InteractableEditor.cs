using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor
{
    SerializedProperty adventureScript;
    SerializedProperty clickLabel;
    SerializedProperty combinations;
    SerializedProperty fallbackCombinationLabel;

    void OnEnable()
    {
        adventureScript = serializedObject.FindProperty("adventureScript");
        clickLabel = serializedObject.FindProperty("clickLabel");
        combinations = serializedObject.FindProperty("combinations");
        fallbackCombinationLabel = serializedObject.FindProperty("fallbackCombinationLabel");

        CombinationDrawer.onClickEditEvent.AddListener(onClickEditCombination);
    }

    private void OnDisable()
    {
        CombinationDrawer.onClickEditEvent.RemoveListener(onClickEditCombination);
    }

    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(adventureScript);
        InteractionEditorUtils.ScriptLabelProperty(clickLabel, interactable.adventureScript, $"OnClick{interactable.gameObject.name}");
        EditorGUILayout.PropertyField(combinations);
        InteractionEditorUtils.ScriptLabelProperty(fallbackCombinationLabel, interactable.adventureScript, $"OnFallback{interactable.gameObject.name}");

        serializedObject.ApplyModifiedProperties();
    }

    void onClickEditCombination(MonoBehaviour monoBehaviour, string label, bool createIfNew)
    {
        Interactable interactable = (Interactable)target;

        if (monoBehaviour == interactable)
        {
            interactable.adventureScript.EditScript(label, createIfNew);
        }
    }
}
