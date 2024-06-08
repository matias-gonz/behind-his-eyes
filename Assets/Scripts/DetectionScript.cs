using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public GameObject target;
    public float _fov = 120f;
    
    void Update()
    {
        if (!target)
        {
            return;
        }

        Vector3 playerDirection = target.transform.position - transform.position;
        playerDirection.y = 0;
        Vector3 lookDirection = transform.forward;
        lookDirection.y = 0;
        float angle = Vector3.Angle(playerDirection, lookDirection);
        if (Math.Abs(angle) < _fov / 2)
        {
            Debug.Log("Target in sight");
        }
    }
}
