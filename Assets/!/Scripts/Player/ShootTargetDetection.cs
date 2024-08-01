using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class ShootTargetDetection : MonoBehaviour
{
    public TimeLinesDirector timeLinesDirector;
    public GameObject camera;
    public GameManager gameManager;
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
            gameManager.TargetHit();
            timeLinesDirector.PlayTimeLine(TimeLine.TutorialOutro);
            Debug.Log("Hit2!!!");
        }
    }
}
