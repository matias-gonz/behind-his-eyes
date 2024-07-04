using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using utils;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<Audio> audioPlayer = new List<Audio>();
    private AudioClip _currentClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayAudio("Walk");
    }

    public void Crouched()
    {
        PlayAudio("Crouched");
    }

    public void Crawl()
    {
        PlayAudio("Crawl");
    }

    public void Run()
    {
        Debug.Log("Run");
        PlayAudio("Walk");
    }

    public void Jump()
    {
        PlayAudio("Jump");
    }

    public void PlayAudio(string clipName)
    {
        // Use the Find method to find the matching audio clip
        Audio? audioToPlay = audioPlayer.Find(a => a.id == clipName);

        // If the audio clip is not found, log a warning message and return
        if (audioToPlay == null || !audioToPlay.HasValue)
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
            return;
        }

        // Get the found audio clip
        _currentClip = audioToPlay.Value.clip;
        Debug.Log(clipName);

        // Play the audio clip
        if (_currentClip != null)
        {
            audioSource.clip = _currentClip;
            audioSource.Play();
            audioSource.loop = false;
        }
    }
}