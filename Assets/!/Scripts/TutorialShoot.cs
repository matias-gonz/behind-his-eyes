using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class TutorialShoot : MonoBehaviour
{
    public float delay = 10f;
    private bool _alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_alreadyTriggered) return;
        
        StartCoroutine(nameof(LoadNextScene));
        _alreadyTriggered = true;
    }
    
    IEnumerator LoadNextScene()
    {
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.LoadScene(Scene.StreetLevel);
                yield break; 
            }
            
            elapsedTime += Time.deltaTime; 
            yield return null;
        }

        GameManager.Instance.LoadScene(Scene.StreetLevel);
    }
}
