using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioPlayer = new List<AudioClip>();
    private AudioClip currentClip;

    private float moveAudioCooldown = 0;
    private float jumpAudioCooldown = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        moveAudioCooldown += Time.deltaTime;
        jumpAudioCooldown += Time.deltaTime;
    }

    public void HandlePlayerAudio(Animator ani, int velocityXHash, int velocityZHash)
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (ani.GetBool("isProne"))
            {
                HandleProneMovement(ani, velocityXHash, velocityZHash);
            }
            else
            {
                HandleWalkingMovement(ani, velocityXHash, velocityZHash);
            }
        }
        else
        {
            HandleRunningMovement(ani, velocityXHash, velocityZHash);
        }

        HandleJumpingMovement(ani);
    }

    private void HandleProneMovement(Animator ani, int velocityXHash, int velocityZHash)
    {
        if ((ani.GetFloat(velocityZHash) != 0 || ani.GetFloat(velocityXHash) != 0) && moveAudioCooldown >= 1f)
        {
            moveAudioCooldown = 0;
            Debug.Log("moveAudioCooldown" + moveAudioCooldown);
            PlayAudio("Prone", 0.01f, true);
        }
    }

    private void HandleWalkingMovement(Animator ani, int velocityXHash, int velocityZHash)
    {
        if ((ani.GetFloat(velocityZHash) != 0 || ani.GetFloat(velocityXHash) != 0) && moveAudioCooldown >= 0.55f)
        {
            moveAudioCooldown = 0;
            Debug.Log("moveAudioCooldown" + moveAudioCooldown);
            PlayAudio("Walk", 0.01f, true);
        }
    }

    private void HandleRunningMovement(Animator ani, int velocityXHash, int velocityZHash)
    {
        if ((ani.GetFloat(velocityZHash) != 0 || ani.GetFloat(velocityXHash) != 0) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayAudio("Run", 0.01f, false);
        }
    }

    private void HandleJumpingMovement(Animator ani)
    {
        if (ani.GetBool("isJump") && jumpAudioCooldown >= 1f)
        {
            jumpAudioCooldown = 0;
            Debug.Log("Jump");
            PlayAudio("Jump", 0.5f, true);
        }
    }

    private void PlayAudio(string clipName, float delay, bool isLooping)
    {
        currentClip = audioPlayer.Find(playerClip => playerClip.name == clipName);
        if (currentClip != null)
        {
            audioSource.clip = currentClip;
            audioSource.loop = isLooping;
            audioSource.PlayDelayed(delay);
        }
        else
        {
            Debug.LogWarning($"Audio clip {clipName} not found!");
        }
    }
}