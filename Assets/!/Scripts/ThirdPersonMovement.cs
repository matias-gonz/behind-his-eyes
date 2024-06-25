using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //components
    private Animator _animator;
    public CharacterController controller;
    public Transform cam;

    // constants
    public float speedMultiplier = 3f;
    public float jumpSpeed = 4f;
    public float gravity = 9.8f;
    public float moveAudioCD = 0;

    // local variable
    private float _vSpeed = 0f; // current vertical velocity
    private float _forwardMovement;
    private float _sidewardMovement;

    //hashes
    private int _velocityXHash;
    private int _velocityZHash;


    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        // turns mouse cursor invisible and locks it in place, allowing indefinite mouse movement
        Cursor.lockState = CursorLockMode.Locked;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _forwardMovement = _animator.GetFloat(_velocityZHash);
        _sidewardMovement = _animator.GetFloat(_velocityXHash);
        // walk direction in normal cordinate system
        Vector3 direction = new Vector3(_sidewardMovement, 0f, _forwardMovement);

        float targetAngle = cam.eulerAngles.y; //Mathf.Atan2(velocityZ, velocityX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        // make sure character moves in direction of target angle
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction; //Vector3.forward;
        //Debug.Log("moveDir" + moveDir);
        controller.Move(moveDir * speedMultiplier * Time.deltaTime);

        moveAudioCD += Time.deltaTime;
        if (moveDir.x != 0 && !Input.GetKey(KeyCode.LeftShift))
        {

            string str = _animator.GetBool("isProne") ? "Climb" : "Walk";
            Debug.Log(str);
            if (moveAudioCD >= 0.55f && str == "Walk")
            {
                StartCoroutine(IsPlayerAudio(str, 0.01f, true));
                moveAudioCD = 0;
            }
            else if (moveAudioCD >= 1f && str == "Climb")
            {
                StartCoroutine(IsPlayerAudio(str, 0.01f, true));
                moveAudioCD = 0;
            }
            audio.loop = false;
        }
        if (moveDir.x != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Run");
            StartCoroutine(IsPlayerAudio("Run", 0.01f, false));
            audio.loop = true;
        }

        if (controller.isGrounded)
        {
            _vSpeed = 0; // grounded character has vSpeed = 0...
            if (Input.GetKeyDown("space"))
            {
                // unless it jumps:
                _vSpeed = jumpSpeed;
                StartCoroutine(IsPlayerAudio("Jump", 0.5f, true));
                audio.loop = false;
                Debug.Log("Jump");
            }
        }
        else
        {
            _vSpeed -= gravity * Time.deltaTime;
        }
        // apply gravity acceleration to vertical speed:

        Vector3 vertMove = new Vector3(0f, _vSpeed, 0f);
        //Debug.Log("vertMove" + vertMove);
        controller.Move(vertMove * Time.deltaTime);
    }



    IEnumerator IsPlayerAudio(string Clip, float time, bool IsOneShot)
    {
        yield return new WaitForSeconds(time);
        if (IsOneShot)
        {
            audio.clip = Resources.Load<AudioClip>("Sounds/" + Clip);
            audio.PlayOneShot(audio.clip);
        }
        else
        {
            if (audio.isPlaying)
            {
                audio.Stop();
                audio.clip = Resources.Load<AudioClip>("Sounds/" + Clip);
                audio.Play();
                audio.volume = 1;
            }
            else
            {
                audio.clip = Resources.Load<AudioClip>("Sounds/" + Clip);
                audio.Play();
            }
        }
    }
}

