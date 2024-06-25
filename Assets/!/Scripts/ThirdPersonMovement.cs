using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //components
    private Animator _animator;
    private Rigidbody _rigidbody;
    public Transform cam;

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
    private float _forwardMovement;
    private float _sidewardMovement;

    // private Vector3 currentPosition;
    private float _jumpStartTime;
    private bool _isGrounded = true;

    //hashes
    private int _velocityXHash;
    private int _velocityZHash;
    private int _isJumpHash;

    // Start is called before the first frame update
    void Start()
    {
        // jumping function y value
        _offsetY = gravity * Mathf.Pow((_offsetX), 2f);
        _colliderCenterY = 0.9f;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _isJumpHash = Animator.StringToHash("isJump");
        // turns mouse cursor invisible and locks it in place, allowing indefinite mouse movement
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _forwardMovement = _animator.GetFloat(_velocityZHash);
        _sidewardMovement = _animator.GetFloat(_velocityXHash);
        // walk direction in normal cordinate system
        Vector3 direction = new Vector3(_sidewardMovement, 0f, _forwardMovement);

        float targetAngle = cam.eulerAngles.y; //Mathf.Atan2(velocityZ, velocityX) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction;
        Vector3 currentPosition = transform.position;
        Vector3 intermediatePosition =
            currentPosition + moveDir * speedMultiplier * Time.fixedDeltaTime;
        // make sure character moves in direction of target angle
        _rigidbody.Move(intermediatePosition, Quaternion.Euler(0f, targetAngle, 0f));
        // _rigidbody.velocity =

        bool isJump = _animator.GetBool(_isJumpHash);
        if (!isJump)
        {
            
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
            {
                float distanceToGround = hit.distance;
                _isGrounded = distanceToGround <= 0.91f;
            }

            if (!_isGrounded)
            {
                Vector3 gravityVector = new Vector3(0f, -9.8f, 0f);
                // _rigidbody.AddForce(gravityVector);
                 _rigidbody.velocity = gravityVector;
            } else
            {
                Debug.Log("is Grounded");
                _rigidbody.AddForce(Vector3.zero);
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }

    //  = controller.isGrounded;
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

    // public void StartJump()
    // {
    //      _rigidbody.useGravity = false;
    // }
    // public void StopJump()
    // {
    //     _rigidbody.useGravity = true;
    // }
}
