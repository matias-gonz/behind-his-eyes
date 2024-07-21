using System.Diagnostics;
using UnityEngine;
using utils;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Audio[] audioPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Walk()
    {
        PlayAudio("Walk", 0.8f);
    }

    public void Run()
    {
        PlayAudio("Walk", 1f);
    }


    public void FallDown()
    {
        PlayAudio("FallDown", 0.7f);
    }
    
    public void FireK98()
    {
        PlayAudio("k98", 1f);
    }

    public void CycleK98()
    {
        PlayAudio("k98cycle", 1f);
    }

    public void SmokeN(int number)
    {
        if (number > 4 || number < 0)
        {
            UnityEngine.Debug.LogWarning("Sound clip id is out of bounds: " + number);
            return;
        }
        UnityEngine.Debug.Log("smoke" + number.ToString());
        PlayAudio("smoke" + number.ToString(), 1f);
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

}
