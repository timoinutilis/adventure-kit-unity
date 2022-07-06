using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public SaveGameContent[] contents;

    public void Save()
    {
        JObject rootObject = new();
        foreach (var content in contents)
        {
            string key = content.SaveGameKey();
            JObject obj = content.ToSaveGameObject();
            rootObject[key] = obj;
        }
        string json = rootObject.ToString();

        string path = Application.persistentDataPath + "/game.json";
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/game.json";
        string json = File.ReadAllText(path);
        JObject rootObject = JObject.Parse(json);

        foreach (var content in contents)
        {
            string key = content.SaveGameKey();
            JObject obj = (JObject)rootObject[key];
            content.FromSaveGameObject(obj);
        }
    }

    public void Reset()
    {
        foreach (var content in contents)
        {
            content.Reset();
        }
    }

}
