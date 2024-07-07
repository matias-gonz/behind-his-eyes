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
    private float _timeFalling = 0f;

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
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
        _rigidbody.AddForce(new Vector3(0f, 0f, 0f));
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
        _rigidbody.MovePosition(intermediatePosition);
        // Debug.Log(intermediatePosition);
        _rigidbody.MoveRotation(Quaternion.Euler(0f, targetAngle, 0f));
        Vector3 resetVelocityXZ = new Vector3(0f, _rigidbody.velocity.y, 0f);
        _rigidbody.velocity = resetVelocityXZ;
    }

    private void VerticalVelocity()
    {
        float distanceToGround = DistanceToGround();
        bool isGrounded = distanceToGround <= 0.2f;
        if (!isGrounded)
        {
            _timeFalling += Time.fixedDeltaTime;
            float velocityY = _rigidbody.velocity.y - 0.1f * 9.81f * _timeFalling;
            Vector3 fallingVelocity = new Vector3(0f, velocityY, 0f);
            _rigidbody.velocity = fallingVelocity;
        }
        else if (distanceToGround == 0f)
        {
            _rigidbody.velocity = Vector3.zero;
            _timeFalling = 0f;
        }
        else
        {
            // all forces and velocities are reset if standing on ground.
            // _rigidbody.velocity = Vector3.zero;
            _rigidbody.velocity = new Vector3(0f, -0.1f, 0f);
            _timeFalling = 0f;
        }
    }

    private float DistanceToGround()
    {
        float distanceToGround;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
        }
        else
        {
            distanceToGround = 0f;
        }
        return distanceToGround;
    }

    public bool JumpingAllowed()
    {
        return DistanceToGround() <= 0.2f;
    }
}
