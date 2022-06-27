using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public SaveGameContent[] contents;

    public void Save()
    {
        foreach (var content in contents)
        {
            string json = content.ToJson();
            string key = content.Key();
            Debug.Log("Save " + key + ": " + json);
        }
    }

    public void Load()
    {

    }

    public void Reset()
    {
        foreach (var content in contents)
        {
            content.Reset();
        }
    }

}
