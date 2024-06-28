using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioPlayer = new List<AudioClip>();
    private AudioClip currentClip;


    private float moveAudioCD = 0;
    private float jumpAudioCD = 0;

    void Start()
    {
        // Initialize if audioSource if needed
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        moveAudioCD += Time.deltaTime;
        jumpAudioCD += Time.deltaTime;
    }

    public void MoveAudio(Animator ani, int _velocityXHash, int _velocityZHash)
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            //if trigger prone condition
            if (ani.GetBool("isProne"))
            {
                //is prone
                if ((ani.GetFloat(_velocityZHash) != 0 || ani.GetFloat(_velocityXHash) != 0) && moveAudioCD >= 1f)
                {
                    moveAudioCD = 0;
                    Debug.Log("moveAudioCD" + moveAudioCD);
                    PlayAudio("Climb", 0.01f, true);
                }
            }
            else //is standing
            {
                if ((ani.GetFloat(_velocityZHash) != 0 || ani.GetFloat(_velocityXHash) != 0) && moveAudioCD >= 0.55f)
                {
                    moveAudioCD = 0;
                    Debug.Log("moveAudioCD" + moveAudioCD);
                    PlayAudio("Walk", 0.01f, true);
                }
            }
        }
        //is running
        if ((ani.GetFloat(_velocityZHash) != 0 || ani.GetFloat(_velocityXHash) != 0) && Input.GetKeyDown(KeyCode.LeftShift)) PlayAudio("Run", 0.01f, false);
        //is jumping
        if (ani.GetBool("isJump") && jumpAudioCD >= 1f)
        {
            jumpAudioCD = 0;
            Debug.Log("Jump");
            PlayAudio("Jump", 0.5f, true);
        }
    }

    public void PlayAudio(string clipName, float delay, bool isOneShot)
    {
        StartCoroutine(PlayAudioCoroutine(clipName, delay, isOneShot));
    }

    private IEnumerator PlayAudioCoroutine(string clipName, float delay, bool isOneShot)
    {
        yield return new WaitForSeconds(delay);

        foreach (AudioClip playerClip in audioPlayer)
        {
            if (playerClip.name == clipName)
            {
                currentClip = playerClip;
                Debug.Log(clipName);
            }
        }

        if (currentClip != null)
        {
            audioSource.clip = currentClip;
            if (isOneShot)
            {
                audioSource.PlayOneShot(currentClip);
                audioSource.loop = false;
            }
            else
            {
                audioSource.Play();
                audioSource.loop = true;
            }
        }
    }
}
