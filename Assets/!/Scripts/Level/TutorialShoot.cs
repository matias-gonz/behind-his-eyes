using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class TutorialShoot : MonoBehaviour
{
    public float delay = 60f;
    public PlayerController playerController;
    public TitleController titleController;
    public VoiceLinesManager voiceLinesManager;
    public PostProcessingController postProcessingController;
    private bool _alreadyTriggered = false;
    

    private void OnTriggerEnter(Collider other)
    {
        playerController.InAimZone(true);
        postProcessingController.swapActiveVolume();
        
        if (titleController.GetActiveTitle() == "shoot") return;
        
        titleController.ShowTitle("aim");
        
        if (_alreadyTriggered) return;
        
        StartCoroutine(nameof(LoadNextScene));
        _alreadyTriggered = true;
        voiceLinesManager.PlayNextLine();
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.InAimZone(false);
        postProcessingController.swapActiveVolume();

        if (titleController.GetActiveTitle() == "shoot") return;
        
        titleController.ShowTitle("move-to-shooting-range");
    }

    IEnumerator LoadNextScene()
    {
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime; 
            yield return null;
        }

        GameManager.Instance.LoadScene(Scene.StoryCards);
    }
}
