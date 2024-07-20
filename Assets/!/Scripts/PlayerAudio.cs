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

    enum RandomClip
    {
        Walk, Run, DieScream, Blood
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayRandomAudio(RandomClip.Walk, 0.8f);
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

    private void PlayRandomAudio(RandomClip clipType, float volume)
    {
        string clipName = RandomAudio(clipType);
        PlayAudio(clipName, volume);
    }

    public void PlayAudio(string clipName, float volume)
    {
        Audio audioToPlay = FindAudioClip(clipName);

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

    private Audio FindAudioClip(string clipName)
    {
        Audio audioToPlay = System.Array.Find(walkAudio, a => a.id == clipName);
        if (audioToPlay.clip == null)
        {
            audioToPlay = System.Array.Find(runAudio, a => a.id == clipName);
        }
        if (audioToPlay.clip == null)
        {
            audioToPlay = System.Array.Find(dieScreamAudio, a => a.id == clipName);
        }
        if (audioToPlay.clip == null)
        {
            audioToPlay = System.Array.Find(bloodAudio, a => a.id == clipName);
        }
        if (audioToPlay.clip == null)
        {
            audioToPlay = System.Array.Find(otherAudio, a => a.id == clipName);
        }
        return audioToPlay;
    }

    private string RandomAudio(RandomClip clipType)
    {
        int index = 0;
        int maxIndex = 0;
        string clipName = "";

        switch (clipType)
        {
            case RandomClip.Walk:
                maxIndex = walkAudio.Length;
                clipName = "walk";
                break;
            case RandomClip.Run:
                maxIndex = runAudio.Length;
                clipName = "run";
                break;
            case RandomClip.DieScream:
                maxIndex = dieScreamAudio.Length;
                clipName = "diescream";
                break;
            case RandomClip.Blood:
                maxIndex = bloodAudio.Length;
                clipName = "blood";
                break;
        }

        if (maxIndex > 0)
        {
            index = Random.Range(1, maxIndex + 1);
        }
        return clipName + index;
    }
}
