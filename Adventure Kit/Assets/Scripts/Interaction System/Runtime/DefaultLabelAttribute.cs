using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLabelAttribute : PropertyAttribute
{
    public string label;

    public DefaultLabelAttribute(string label)
    {
        this.label = label;
    }
}
