using System.Collections.Generic;
using UnityEngine;
using utils;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Audio[] walkAudio;
    public Audio[] runAudio;
    public Audio[] dieScreamAudio;
    public Audio[] bloodAudio;
    public Audio[] otherAudio;
    private Dictionary<RandomClip, Audio[]> audioClipDictionary;

    enum RandomClip
    {
        Walk, Run, DieScream, Blood, Other
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeAudioClipDictionary();
    }

    private void InitializeAudioClipDictionary()
    {
        audioClipDictionary = new Dictionary<RandomClip, Audio[]>
        {
            { RandomClip.Walk, walkAudio },
            { RandomClip.Run, runAudio },
            { RandomClip.DieScream, dieScreamAudio },
            { RandomClip.Blood, bloodAudio },
            { RandomClip.Other, otherAudio }
        };
    }

    public void Walk()
    {
        PlayRandomAudio(RandomClip.Walk, 0.8f);
    }

    public void LeftCrouched()
    {
        PlayAudio(FindAudioClip("LeftCrouch"), 0.5f);
    }

    public void RightCrouched()
    {
        PlayAudio(FindAudioClip("RightCrouch"), 0.5f);
    }

    public void Crawl()
    {
        PlayAudio(FindAudioClip("Crawl"), 0.20f);
    }

    public void Run()
    {
        PlayRandomAudio(RandomClip.Run, 1f);
    }

    public void DieScream()
    {
        PlayRandomAudio(RandomClip.DieScream, 0.8f);
    }

    public void Blood()
    {
        PlayRandomAudio(RandomClip.Blood, 3.5f);
    }

    public void BulletHit()
    {
        PlayAudio(FindAudioClip("BulletHit"), 1.5f);
    }

    public void Jump()
    {
        PlayAudio(FindAudioClip("Jump"), 1f);
    }

    public void FallDown()
    {
        PlayAudio(FindAudioClip("FallDown"), 0.7f);
    }

    private void PlayRandomAudio(RandomClip clipType, float volume)
    {
        Audio audioClip = GetRandomAudioClip(clipType);
        PlayAudio(audioClip, volume);
    }

    public void PlayAudio(Audio audioClip, float volume)
    {
        if (audioClip.clip == null)
        {
            UnityEngine.Debug.LogWarning("Audio clip not found: " + audioClip.id);
            return;
        }

        audioSource.clip = audioClip.clip;
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.loop = false;
    }

    private Audio FindAudioClip(string clipName)
    {
        foreach (var audio in walkAudio)
        {
            if (audio.id == clipName)
            {
                return audio;
            }
        }
        foreach (var audio in runAudio)
        {
            if (audio.id == clipName)
            {
                return audio;
            }
        }
        foreach (var audio in dieScreamAudio)
        {
            if (audio.id == clipName)
            {
                return audio;
            }
        }
        foreach (var audio in bloodAudio)
        {
            if (audio.id == clipName)
            {
                return audio;
            }
        }
        foreach (var audio in otherAudio)
        {
            if (audio.id == clipName)
            {
                return audio;
            }
        }

        return new Audio();
    }

    private Audio GetRandomAudioClip(RandomClip clipType)
    {
        if (audioClipDictionary.TryGetValue(clipType, out var clips))
        {
            int index = Random.Range(0, clips.Length);
            return clips[index];
        }

        return new Audio();
    }
}
