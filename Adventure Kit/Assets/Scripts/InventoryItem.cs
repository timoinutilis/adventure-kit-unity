using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject
{
    public string displayName;
    public Sprite sprite;
    public string defaultInteractionLabel;
    public CombinationCollection combinationCollection = new();
}
