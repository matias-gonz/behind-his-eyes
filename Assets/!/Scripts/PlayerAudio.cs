using System.Collections.Generic;
using UnityEngine;
using utils;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource AudioSource;

    public Audio[] WalkAudio;
    public Audio[] RunAudio;
    public Audio[] DieScreamAudio;
    public Audio[] BloodAudio;
    public Audio[] LeftCrouchAudio;
    public Audio[] RightCrouchAudio;
    public Audio[] CrawlAudio;
    public Audio[] BulletHitAudio;
    public Audio[] JumpAudio;
    public Audio[] FallDownAudio;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayRandomAudio(WalkAudio, 0.8f);
    }

    public void LeftCrouched()
    {
        PlayRandomAudio(LeftCrouchAudio, 0.5f);
    }

    public void RightCrouched()
    {
        PlayRandomAudio(RightCrouchAudio, 0.5f);
    }

    public void Crawl()
    {
        PlayRandomAudio(CrawlAudio, 0.20f);
    }

    public void Run()
    {
        PlayRandomAudio(RunAudio, 1f);
    }

    public void DieScream()
    {
        PlayRandomAudio(DieScreamAudio, 0.8f);
    }

    public void Blood()
    {
        PlayRandomAudio(BloodAudio, 3.5f);
    }

    public void BulletHit()
    {
        PlayRandomAudio(BulletHitAudio, 1.5f);
    }

    public void Jump()
    {
        PlayRandomAudio(JumpAudio, 1f);
    }

    public void FallDown()
    {
        PlayRandomAudio(FallDownAudio, 0.7f);
    }

    private void PlayRandomAudio(Audio[] audioArray, float volume)
    {
        if (audioArray.Length == 0)
        {
            Debug.LogWarning("Audio array is empty");
            return;
        }

        Audio audioClip = audioArray.Length == 1 ? audioArray[0] : audioArray[Random.Range(0, audioArray.Length)];
        PlayAudio(audioClip, volume);
    }

    public void PlayAudio(Audio audioClip, float volume)
    {
        if (audioClip.clip == null)
        {
            Debug.LogWarning("Audio clip not found: " + audioClip.id);
            return;
        }

        AudioSource.clip = audioClip.clip;
        AudioSource.volume = volume;
        AudioSource.Play();
        AudioSource.loop = false;
    }
}
