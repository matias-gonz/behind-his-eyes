using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class ShootTargetDetection : MonoBehaviour
{
    public TimeLinesDirector timeLinesDirector;
    public GameObject camera;
    private LayerMask _layerMask;

    void Start()
    {
        _layerMask = LayerMask.GetMask("ShootingTarget");
    }

    public void DetectHit()
    {
        Vector3 rayStart = camera.transform.position;
        Vector3 rayDirection = camera.transform.forward;
        if (Physics.Raycast(rayStart, rayDirection, out _, 100, _layerMask))
        {
            Debug.Log("Hit1!!!");
            GameManager.Instance.TargetHit();
            timeLinesDirector.PlayTimeLine(TimeLine.TutorialOutro);
        }
    }
}
