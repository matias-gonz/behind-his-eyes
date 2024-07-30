using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTargetDetection : MonoBehaviour
{
    public GameObject camera;
    private LayerMask _layerMask;

    void Start()
    {
        _layerMask = LayerMask.GetMask("ShootingTarget");
    }

    public void DetectHit()
    {
        Debug.Log("Detecting hit");
        Vector3 rayStart = camera.transform.position;
        Vector3 rayDirection = camera.transform.forward;
        if (Physics.Raycast(rayStart, rayDirection, out _, 100, _layerMask))
        {
            Debug.Log("Hit!!!");
        }
    }
}
