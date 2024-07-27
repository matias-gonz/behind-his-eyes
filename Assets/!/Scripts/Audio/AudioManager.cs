using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Audio[] soundFxs;
    public AudioSource audioSource;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFx(string id)
    {
        if (audioSource.isPlaying) return;
        
        Audio sound = Array.Find(soundFxs, soundFx => soundFx.id == id);
        if (!sound.clip) return;
        
        audioSource.PlayOneShot(sound.clip);
    }
}
