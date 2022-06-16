using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform location;
    public AdventureScript adventureScript;
    public string clickLabel;
    //TODO: use-with-item labels

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        GlobalScriptPlayer.Instance.Execute(adventureScript, clickLabel);
    }
}
