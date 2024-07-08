using System.Diagnostics;
using UnityEngine;
using utils; 

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Audio[] audioPlayer;
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
        Audio? audioToPlay = System.Array.Find(audioPlayer, a => a.id == clipName);

        // Add the check for audioToPlay.Value.clip being null into this guard clause
        if (audioToPlay == null || audioToPlay.Value.clip == null)
        {
            UnityEngine.Debug.LogWarning("Audio clip not found: " + clipName);
            return;
        }

        // Assign and check _currentClip in one step
        _currentClip = audioToPlay.Value.clip;
        if (_currentClip == null)
        {
            UnityEngine.Debug.LogWarning("Audio clip is null or not loaded: " + clipName);
            return;
        }

        // Now we're sure _currentClip is not null, proceed to play it
        audioSource.clip = _currentClip;
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.loop = false;
    }

}

