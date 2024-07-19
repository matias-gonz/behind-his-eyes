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


    enum RandomClip
    {
        walkOrRun, diescream, blood
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayAudio(RandomAudio("walk", (int)RandomClip.walkOrRun), 0.8f);
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
        PlayAudio(RandomAudio("run", (int)RandomClip.walkOrRun), 1f);
    }

    public void DieScream()
    {
        PlayAudio(RandomAudio("diescream", (int)RandomClip.diescream), 0.8f);
    }

    public void Blood()
    {
        PlayAudio(RandomAudio("blood", (int)RandomClip.blood), 3.5f);
    }

    public void BulletHit()
    {
        PlayAudio("BulletHit", 1.5f);
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


    private string RandomAudio(string clipName, int indexClip)
    {
        int index = 0;
        switch (indexClip)
        {
            case 0:
                index = Random.Range(1, 6);
                break;
            case 1:
                index = Random.Range(1, 4);
                break;
            case 2:
                index = Random.Range(1, 3);
                break;
        }
        return clipName + index;
    }

}
