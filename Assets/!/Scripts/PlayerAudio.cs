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
        PlayAudio(RandomAudio("walk"), 0.8f, true);
    }

    public void LeftCrouched()
    {
        PlayAudio("LeftCrouch", 0.5f, false);
    }

    
    public void RightCrouched()
    {
        PlayAudio("RightCrouch", 0.5f, false);
    }


    public void Crawl()
    {
        PlayAudio("Crawl", 0.20f, false);
    }

    public void Run()
    {
        PlayAudio(RandomAudio("run"), 1f, true);
    }

    public void Jump()
    {
        PlayAudio("Jump", 1f, false);
    }

    public void FallDown()
    {
        PlayAudio("FallDown", 0.7f, false);
    }

    public void PlayAudio(string clipName, float volume, bool isRandom)
    {
        Audio audioToPlay = System.Array.Find(audioPlayer, a => a.id == clipName);
        if (isRandom)
        {
            foreach (AudioClip audioClip in audioClips)
            {
                System.Console.WriteLine(audioClip.name);
                if (!DicAudioPlayer.ContainsKey(audioClip.name))
                {
                    DicAudioPlayer.Add(audioClip.name, audioClip);
                }
            }
            audioToPlay.clip = DicAudioPlayer[clipName];
        }
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



