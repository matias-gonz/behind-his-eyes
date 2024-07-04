using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioPlayer = new List<AudioClip>();
    private AudioClip currentClip;

    void Start()
    {
        // initialize audioSource
        audioSource = GetComponent<AudioSource>();
    }

    //walk
    public void Walk()
    {
        PlayAudio("Walk");
    }
    //crouch
    public void Crouched()
    {
        PlayAudio("Crouched");
    }
    //crawl
    public void Crawl()
    {
        PlayAudio("Crawl");
    }
    //run
    public void Run()
    {
        Debug.Log("Run");
        PlayAudio("Walk");
    }
    //jump
    public void Jump()
    {
        PlayAudio("Jump");
    }

    public void PlayAudio(string clipName)
    {
        foreach (AudioClip playerClip in audioPlayer)
        {
            if (playerClip.name == clipName)
            {
                currentClip = playerClip;
                Debug.Log(clipName);
            }
        }

        if (currentClip != null)
        {
            audioSource.clip = currentClip;
            audioSource.Play();
            audioSource.loop = false;
        }
    }
}
