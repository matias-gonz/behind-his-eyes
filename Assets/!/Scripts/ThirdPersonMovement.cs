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
    public float gravity = 9.81f;

    // local variables
    private float _forwardMovement;
    private float _sidewardMovement;
    private bool _isGrounded = true;

    //hashes
    private int _velocityXHash;
    private int _velocityZHash;
    private int _isJumpHash;

    // Start is called before the first frame update
    void Start()
    {
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
        MoveXZandTurn();
        if (!_animator.GetBool(_isJumpHash))
        {
            VerticalVelocity();
        }
    }

    private void MoveXZandTurn()
    {
        _forwardMovement = _animator.GetFloat(_velocityZHash);
        _sidewardMovement = _animator.GetFloat(_velocityXHash);

        // walk direction in normal cordinate system
        Vector3 direction = new Vector3(_sidewardMovement, 0f, _forwardMovement);
        float targetAngle = cam.eulerAngles.y;

        //direction in relation to camera including for strafe walking
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction;
        Vector3 currentPosition = transform.position;
        Vector3 intermediatePosition =
            currentPosition + moveDir * speedMultiplier * Time.fixedDeltaTime;
        // make sure character moves in direction of target angle, i.e. where the camera is looking
        _rigidbody.Move(intermediatePosition, Quaternion.Euler(0f, targetAngle, 0f));
    }

    private void VerticalVelocity()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            float distanceToGround = hit.distance;
            _isGrounded = distanceToGround <= 0.02f;
        }

        if (!_isGrounded)
        {
            Vector3 gravityVector = new Vector3(0f, -9.8f, 0f);
            _rigidbody.AddForce(gravityVector);
            // _rigidbody.velocity = gravityVector;
        }
        else
        {
            // all forces and velocities are reset if standing on ground.
            _rigidbody.AddForce(Vector3.zero);
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }
}
