using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Interactable interactable = (Interactable)target;
        if (interactable.adventureScript == null)
        {
            if (GUILayout.Button("Create Script"))
            {
                interactable.adventureScript = ScriptableObject.CreateInstance<AdventureScript>();
            }
        }
    }
}
