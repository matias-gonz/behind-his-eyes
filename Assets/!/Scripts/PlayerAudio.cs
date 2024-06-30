using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils; // 确保导入了 utils 命名空间

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<Audio> audioPlayer = new List<Audio>(); // 使用 Audio 结构体
    private AudioClip currentClip;

    private float moveAudioCooldown = 0;
    private float jumpAudioCooldown = 0;

    public float proneDelay = 0.01f; // 序列化延迟变量
    public float walkDelay = 0.01f; // 序列化延迟变量
    public float runDelay = 0.01f; // 序列化延迟变量
    public float jumpDelay = 0.5f; // 序列化延迟变量

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
        if (ani.GetBool("isJump"))
        {
            HandleJumpingMovement(ani);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            HandleRunningMovement(ani, velocityXHash, velocityZHash);
        }
        else if (ani.GetBool("isProne"))
        {
            HandleProneMovement(ani, velocityXHash, velocityZHash);
        }
        else
        {
            HandleWalkingMovement(ani, velocityXHash, velocityZHash);
        }
    }

    private void HandleProneMovement(Animator ani, int velocityXHash, int velocityZHash)
    {
        if (ani.GetFloat(velocityZHash) != 0 || ani.GetFloat(velocityXHash) != 0)
        {
            if (moveAudioCooldown >= 1f)
            {
                moveAudioCooldown = 0;
                PlayAudio("Prone", proneDelay, true);
            }
        }
        else
        {
            StopAudio();
        }
    }

    private void HandleWalkingMovement(Animator ani, int velocityXHash, int velocityZHash)
    {
        if (ani.GetFloat(velocityZHash) != 0 || ani.GetFloat(velocityXHash) != 0)
        {
            if (moveAudioCooldown >= 0.55f)
            {
                moveAudioCooldown = 0;
                PlayAudio("Walk", walkDelay, true);
            }
        }
        else
        {
            StopAudio();
        }
    }

    private void HandleRunningMovement(Animator ani, int velocityXHash, int velocityZHash)
    {
        if (ani.GetFloat(velocityZHash) != 0 || ani.GetFloat(velocityXHash) != 0)
        {
            if (audioSource.clip == null || audioSource.clip.name != "Run")
            {
                PlayAudio("Run", runDelay, true);
            }
        }
        else
        {
            StopAudio();
        }
    }

    private void HandleJumpingMovement(Animator ani)
    {
        if (jumpAudioCooldown >= 1f)
        {
            jumpAudioCooldown = 0;
            PlayAudio("Jump", jumpDelay, false);
        }
    }

    private void PlayAudio(string clipId, float delay, bool isLooping)
    {
        Audio audio = audioPlayer.Find(playerAudio => playerAudio.id == clipId);
        if (audio.clip != null)
        {
            currentClip = audio.clip;
            audioSource.Stop(); // 停止当前播放的音频
            audioSource.clip = currentClip;
            audioSource.loop = isLooping;
            audioSource.PlayDelayed(delay);
        }
        else
        {
            Debug.LogWarning($"Audio clip {clipId} not found!");
        }
    }

    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
