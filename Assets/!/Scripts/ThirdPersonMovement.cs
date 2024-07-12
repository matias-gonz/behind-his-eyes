using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //components
    private Rigidbody _rigidbody;
    public Transform cam;
    public TwoDimensionalAnimationStateController _animationController;

    // constants
    public const float SpeedMultiplier = 3f;
    public const float Gravity = 9.81f;
    private const float TurnSmoothTime = 0.2f;
    private float _angularVelocity;

    // local variables
    private float _timeFalling = 0f;
    private bool _isGrounded;

    //hashes
    private int _velocityXHash;
    private int _velocityZHash;
    private int _isJumpHash;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VerticalVelocity();
        MoveXZandTurn();
        _rigidbody.AddForce(new Vector3(0f, 0f, 0f));
    }

    private void MoveXZandTurn()
    {
        float _forwardMovement = _animationController.GetVelocityZ();
        float _sidewardMovement = _animationController.GetVelocityX();
        float speed =
            Mathf.Max(Mathf.Abs(_forwardMovement), Mathf.Abs(_sidewardMovement)) * SpeedMultiplier;

        if (speed == 0 && DistanceToGround() == 0)
        {
            _rigidbody.isKinematic = true;
            return;
        }
        else
            _rigidbody.isKinematic = false;
        if (speed == 0)
            return;

        // walk direction in normal cordinate system
        Vector3 direction = new Vector3(_sidewardMovement, 0f, _forwardMovement).normalized;
        float targetAngle = RotateToCurrentTarget(cam.eulerAngles.y);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction;
        Vector3 currentPosition = transform.position;
        Vector3 intermediatePosition = currentPosition + moveDir * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(intermediatePosition);

        Vector3 resetVelocityXZ = new Vector3(0f, _rigidbody.velocity.y, 0f);
        _rigidbody.velocity = resetVelocityXZ;
    }

    private float RotateToCurrentTarget(float targetAngle)
    {
        // float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(
            this.transform.eulerAngles.y,
            targetAngle,
            ref _angularVelocity,
            TurnSmoothTime
        );
        _rigidbody.MoveRotation(Quaternion.Euler(0f, angle, 0f));
        return angle;
    }

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }

    private void VerticalVelocity()
    {
        if (_animationController.IsJumping())
        {
            _rigidbody.velocity = Vector3.zero;
            _timeFalling = 0.5f;
        }
        else
        {
            float distanceToGround = DistanceToGround();
            _isGrounded = distanceToGround <= 0.2f;
            if (!_isGrounded)
            {
                FreeFall();
            }
            else if (distanceToGround == 0f)
            {
                // _rigidbody.velocity = Vector3.zero;
                _timeFalling = 0f;
            }
            else
            {
                _rigidbody.velocity = new Vector3(0f, -0.1f, 0f);
                _timeFalling = 0f;
            }
        }
    }

    private void FreeFall()
    {
        _timeFalling += Time.fixedDeltaTime;
        float velocityY = _rigidbody.velocity.y - 0.1f * Gravity * _timeFalling;
        Vector3 fallingVelocity = new Vector3(0f, velocityY, 0f);
        _rigidbody.velocity = fallingVelocity;
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
}
