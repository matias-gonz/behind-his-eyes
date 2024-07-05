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
        PlayAudio("Walk", 1f);
    }

    public void Crouched()
    {
        PlayAudio("Crouched", 0.5f);
    }

    public void Crawl()
    {
        PlayAudio("Crawl", 0.15f);
    }

    public void Run()
    {
        PlayAudio("Walk", 1f);
    }

    public void Jump()
    {
        PlayAudio("Jump", 1f);
    }

    public void PlayAudio(string clipName, float volume)
    {
        Audio? audioToPlay = audioPlayer.Find(a => a.id == clipName);

        if (audioToPlay == null || !audioToPlay.HasValue)
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
            return;
        }

        _currentClip = audioToPlay.Value.clip;

        if (_currentClip != null)
        {
            audioSource.clip = _currentClip;
            audioSource.volume = volume;
            audioSource.Play();
            audioSource.loop = false;

        }
    }
}