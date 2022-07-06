using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class SaveGameContent : MonoBehaviour
{
    public abstract string SaveGameKey();
    public abstract JObject ToSaveGameObject();
    public abstract void FromSaveGameObject(JObject obj);
    public abstract void Reset();
}
