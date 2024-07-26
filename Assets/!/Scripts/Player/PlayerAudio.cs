using System.Collections.Generic;
using UnityEngine;
using utils;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public Audio[] stepAudio;
    public Audio[] dieScreamAudio;
    public Audio[] bloodAudio;
    public Audio[] crawlAudio;
    public Audio[] bulletHitAudio;
    public Audio[] jumpAudio;
    public Audio[] fallDownAudio;
    public Audio[] fireK98Audio;
    public Audio[] k98CycleAudio;
    public Audio[] voiceLines; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayRandomAudio(stepAudio, 0.5f);
    }

    public void Crouched()
    {
        PlayRandomAudio(stepAudio, 0.35f);
    }

    public void Crawl()
    {
        PlayRandomAudio(crawlAudio, 0.20f);
    }

    public void Run()
    {
        PlayRandomAudio(stepAudio, 0.8f);
    }

    public void DieScream()
    {
        PlayRandomAudio(dieScreamAudio, 0.8f);
    }

    public void Blood()
    {
        PlayRandomAudio(bloodAudio, 3.5f);
    }

    public void BulletHit()
    {
        PlayRandomAudio(bulletHitAudio, 1.5f);
    }

    public void Jump()
    {
        PlayRandomAudio(jumpAudio, 1f);
    }

    public void FallDown()
    {
        PlayRandomAudio(fallDownAudio, 0.7f);
    }

    public void FireK98()
    {
        PlayRandomAudio(fireK98Audio, 1f);
    }

    public void CycleK98()
    {
        PlayRandomAudio(k98CycleAudio, 1f);
    }

    public void SmokeN(int number)
    {
        number -= 1;

        if (number >= voiceLines.Length || number < 0)
        {
            UnityEngine.Debug.LogWarning("Sound clip id is out of bounds: " + number);
            return;
        }
        PlayAudio(voiceLines[number], 1f);
    }


    private void PlayRandomAudio(Audio[] audioArray, float volume)
    {
        if (audioArray.Length == 0)
        {
            Debug.LogWarning("Audio array is empty");
            return;
        }

        Audio audioClip = audioArray[Random.Range(0, audioArray.Length)];
        PlayAudio(audioClip, volume);
    }

    public void PlayAudio(Audio audioClip, float volume)
    {
        if (audioClip.clip == null)
        {
            Debug.LogWarning("Audio clip not found: " + audioClip.id);
            return;
        }

        audioSource.clip = audioClip.clip;
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.loop = false;
    }
}

