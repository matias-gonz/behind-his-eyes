using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public GameObject target;
    public float fov = 120f;
    public float viewDistance = 10f;
    private LayerMask _layerMask;

    private void Start()
    {
        _layerMask = LayerMask.GetMask("Level");
    }

    void Update()
    {
        if (!target)
        {
            return;
        }

        // Check if the target is in range
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > viewDistance) return;

        // Check if the target is in the field of view
        Vector3 playerDirection = target.transform.position - transform.position;
        playerDirection.y = 0;
        Vector3 lookDirection = transform.forward;
        lookDirection.y = 0;
        float angle = Vector3.Angle(playerDirection, lookDirection);
        if (angle > fov / 2) return;

        // Check if the target is in sight
        // TODO: Adjust the ray start and direction once enemy prefab is added
        Vector3 rayStart = transform.position;
        rayStart.y = 1;
        Debug.DrawRay(rayStart, distance * playerDirection, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(rayStart, playerDirection, out hit, distance, _layerMask)) return;

        Debug.Log("Target in sight");
    }
}
