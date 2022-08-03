using UnityEngine;

public abstract class VariableManager : MonoBehaviour
{
    public abstract string GetValueForKey(string key);
    public abstract void SetValueForKey(string key, string value);
}
