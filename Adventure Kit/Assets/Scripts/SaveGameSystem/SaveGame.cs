using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public Saver[] savers;

    public void Save()
    {
        JObject rootObject = new();
        foreach (var saver in savers)
        {
            string key = saver.SaveGameKey();
            JObject obj = saver.ToSaveGameObject();
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

        foreach (var saver in savers)
        {
            string key = saver.SaveGameKey();
            JObject obj = (JObject)rootObject[key];
            saver.FromSaveGameObject(obj);
        }
    }

    public void Reset()
    {
        foreach (var saver in savers)
        {
            saver.Reset();
        }
    }

}
