using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScriptPlayer : MonoBehaviour
{
    public AdventureScript adventureScript;
    public string startLabel;
    public bool startsImmediately;
    public bool isLoopEnabled;

    private ScriptPlayer scriptPlayer;

    private void Awake()
    {
        scriptPlayer = new ScriptPlayer(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startsImmediately)
        {
            Execute();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute()
    {
        scriptPlayer.Execute(adventureScript, startLabel, isLoopEnabled);
    }
}
