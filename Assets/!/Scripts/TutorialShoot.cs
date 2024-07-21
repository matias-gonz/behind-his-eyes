using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class TutorialShoot : MonoBehaviour
{
    public float delay = 10f;
    public PlayerController playerController;
    public TitleController titleController;
    private bool _alreadyTriggered = false;
    

    private void OnTriggerEnter(Collider other)
    {
        playerController.InAimZone(true);
        titleController.ShowTitle("shoot");
        
        if (_alreadyTriggered) return;
        
        StartCoroutine(nameof(LoadNextScene));
        _alreadyTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.InAimZone(false);
        titleController.ShowTitle("move-to-shooting-range");
    }

    IEnumerator LoadNextScene()
    {
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.AddShot();
                yield break; 
            }
            
            elapsedTime += Time.deltaTime; 
            yield return null;
        }

        GameManager.Instance.LoadScene(Scene.StreetLevel);
    }
}
