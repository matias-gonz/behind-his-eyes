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
    public Audio[] leftCrouchAudio;
    public Audio[] rightCrouchAudio;
    public Audio[] crawlAudio;
    public Audio[] bulletHitAudio;
    public Audio[] jumpAudio;
    public Audio[] fallDownAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayRandomAudio(walkAudio, 0.8f);
    }

    public void LeftCrouched()
    {
        PlayRandomAudio(leftCrouchAudio, 0.5f);
    }

    public void RightCrouched()
    {
        PlayRandomAudio(rightCrouchAudio, 0.5f);
    }

    public void Crawl()
    {
        PlayRandomAudio(crawlAudio, 0.20f);
    }

    public void Run()
    {
        PlayRandomAudio(runAudio, 1f);
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
