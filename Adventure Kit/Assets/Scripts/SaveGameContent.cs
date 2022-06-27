using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveGameContent : MonoBehaviour
{
    public abstract string Key();
    public abstract string ToJson();
    public abstract void FromJson(string json);
    public abstract void Reset();
}
