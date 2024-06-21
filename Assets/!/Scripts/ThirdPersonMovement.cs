using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //components
    private Animator _animator;
    private Rigidbody _rigidbody;
    public Transform cam;
    public Collider standingCollider;

    // constants
    public float speedMultiplier = 3f;
    public float jumpSpeed = 4f;
    public float gravity = 9.81f;

    // local variable
    // current vertical velocity, used for falling but not jumping
    private float _vSpeed = 0f;

    //jumping collider function variables, this corresponds to jumping time/2
    private float _offsetX = 0.35f;

    // jumping hight
    private float _offsetY;
    private float _colliderCenterY;
    private float _defaultStepOffset;
    private float _forwardMovement;
    private float _sidewardMovement;
    private float _jumpStartTime;

    //hashes
    private int _velocityXHash;
    private int _velocityZHash;
    private int _isJumpHash;

    // Start is called before the first frame update
    void Start()
    {
        // jumping function y value
        _offsetY = gravity * Mathf.Pow((_offsetX), 2f);
        _colliderCenterY = 0.92f;
        _defaultStepOffset = 0.3f;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _isJumpHash = Animator.StringToHash("isJump");
        // turns mouse cursor invisible and locks it in place, allowing indefinite mouse movement
        Cursor.lockState = CursorLockMode.Locked;
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

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction;
        Vector3 currentPosition = transform.position;
        Vector3 intermediatePosition = currentPosition + moveDir * speedMultiplier * Time.deltaTime;
        // make sure character moves in direction of target angle
        _rigidbody.MovePosition(intermediatePosition);

        // bool _isGrounded = controller.isGrounded;
        // if (_animator.GetBool(_isJumpHash))
        // {
        //     // jumpAdjustCollider();
        //     _vSpeed = 0f;
        // }
        // else if (_isGrounded)
        // {
        //     _vSpeed = 0f; // grounded character has vSpeed = 0...
        //     //     _vSpeed = jumpSpeed;
        // }
        // else
        // {
        //     _vSpeed -= gravity * Time.deltaTime;
        // }
        // // apply gravity acceleration to vertical speed:
        // Vector3 vertMove = new Vector3(0f, _vSpeed, 0f);
        // controller.Move(vertMove * Time.deltaTime);
    }

    public void StartJump()
    {
        _jumpStartTime = Time.time;
        // controller.stepOffset = 0f;
    }

    public void StopJump()
    {
        controller.center = new Vector3(0f, _colliderCenterY, 0f);
        controller.stepOffset = _defaultStepOffset;
    }

    private void jumpAdjustCollider()
    {
        float deltaT = Time.time - _jumpStartTime;
        if (deltaT < 2 * _offsetX)
        {
            float currentHeight = -gravity * Mathf.Pow((deltaT - _offsetX), 2f) + _offsetY;
            controller.center = new Vector3(0f, _colliderCenterY + 0.7f * currentHeight, 0f);
        }
        else
        {
            controller.center = new Vector3(0f, _colliderCenterY, 0f);
        }
    }
}
