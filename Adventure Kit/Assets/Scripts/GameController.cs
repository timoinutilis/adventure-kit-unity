using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Awake()
    {
        ScriptPlayerSettings settings = ScriptPlayerSettings.Instance;
        settings.variableManager = new VariableManager();
        settings.AddCommand(new SayCommand());
        settings.AddCommand(new WalkCommand());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
