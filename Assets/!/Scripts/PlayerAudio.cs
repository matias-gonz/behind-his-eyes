using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using utils;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Audio[] audioPlayer;
    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> dicAudioPlayer = new Dictionary<string, AudioClip>();

    public Dictionary<string, AudioClip> DicAudioPlayer { get => dicAudioPlayer; set => dicAudioPlayer = value; }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayAudio(RandomAudio("walk"), 0.8f);
    }

    public void LeftCrouched()
    {
        PlayAudio("LeftCrouch", 0.5f);
    }

    
    public void RightCrouched()
    {
        PlayAudio("RightCrouch", 0.5f);
    }


    public void Crawl()
    {
        PlayAudio("Crawl", 0.20f);
    }

    public void Run()
    {
        PlayAudio(RandomAudio("run"), 1f);
    }

    public void Jump()
    {
        PlayAudio("Jump", 1f);
    }

    public void FallDown()
    {
        PlayAudio("FallDown", 0.7f);
    }

    public void PlayAudio(string clipName, float volume)
    {
        Audio audioToPlay = System.Array.Find(audioPlayer, a => a.id == clipName);
        if (audioToPlay.clip == null)
        {
            UnityEngine.Debug.LogWarning("Audio clip not found: " + clipName);
            return;
        }
        audioSource.clip = audioToPlay.clip;
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.loop = false;
    }


    private string RandomAudio(string clipName)
    {
        int index = Random.Range(1, 6);
        return clipName + index;
    }

}



