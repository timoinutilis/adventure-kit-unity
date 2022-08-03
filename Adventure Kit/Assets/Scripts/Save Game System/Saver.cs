using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    public abstract string SaveGameKey { get; }
    public abstract JObject ToSaveGameObject();
    public abstract void FromSaveGameObject(JObject obj);
    public abstract void Reset();
}
