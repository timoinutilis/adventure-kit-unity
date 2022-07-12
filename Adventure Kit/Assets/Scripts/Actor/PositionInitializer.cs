using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string positionName = LocationManager.Instance.PositionName;
        if (!String.IsNullOrEmpty(positionName))
        {
            Transform targetTransform = GameObject.Find(positionName).transform;
            transform.position = targetTransform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
