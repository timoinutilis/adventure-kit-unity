using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JVector3
{
    public float x;
    public float y;
    public float z;

    public JVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
