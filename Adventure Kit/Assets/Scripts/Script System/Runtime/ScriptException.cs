using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptException : Exception
{
    public string File { get; set; }
    public int Line { get; set; }

    public ScriptException(string message) : base(message)
    {
    }
}
