using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    Animator _animator;
    public float walkingSpeed = 1.5f;
    float _runningSpeed; //must be 4 times higher than speed
    public float animationMaxSpeed = 2f;
    float _animationSpeed = 0f;
    float _currentSpeed; //formerly speed
    float _currentMaxSpeed;
    readonly float acceleration = 2f;

    private Rigidbody _rigidbody;
    private float _angularVelocity;
    private readonly float _turnSmoothTime = 0.3f;
    private Vector3 _direction;
    private Vector3 _currentTargetPosition;
    int _velocityXHash; //sidewards animation, can be use for strafe walking
    int _velocityZHash; //forwards animation

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _currentSpeed = walkingSpeed; //for testing purposes
        _runningSpeed = 4 * walkingSpeed;
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        CalculateDirection();
        if (_direction == Vector3.zero)
        {
            Debug.Log("_direction is zero!!!");
            return;
        }

        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _angularVelocity,
            _turnSmoothTime);
        Vector3 currentPosition = transform.position;
        //adjust speed when starting to move or getting close to the goal position
        float distanceFromGoal = Vector3.Distance(currentPosition, _currentTargetPosition);
        if (distanceFromGoal < _currentSpeed)
        {
            _currentSpeed -= acceleration * Time.fixedDeltaTime;
        }
        else if (_currentSpeed < _currentMaxSpeed)
        {
            _currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else if (_currentSpeed > _currentMaxSpeed)
        {
            _currentSpeed = _currentMaxSpeed;
        }

        if (_currentSpeed < 0f)
        {
            _currentSpeed = 0f;
        }

        Vector3 intermediatePosition = currentPosition + _direction * (_currentSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(intermediatePosition);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        // maps  to animation max speed boundaries
        if (_animator)
        {
            _animationSpeed = (_currentSpeed / _runningSpeed) * animationMaxSpeed;
            _animator.SetFloat(_velocityZHash, _animationSpeed);
        }
    }

    public void MoveTo(Vector3 position, bool isRunning)
    {
        //Debug.Log("Guard: New Target to MoveTo");
        _currentTargetPosition = position;
        _currentMaxSpeed = isRunning ? _runningSpeed : walkingSpeed;
    }

    public void Stop()
    {
        _currentTargetPosition = transform.position;
    }

    private void CalculateDirection()
    {
        Vector3 direction = (_currentTargetPosition - transform.position).normalized;
        _direction = direction;
    }
}
