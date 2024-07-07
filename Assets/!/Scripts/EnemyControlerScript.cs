using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    private Animator _animator;
    private TwoDimensionalAnimationStateController _twoDimensionalAnimationStateController;
    public float walkingSpeed = 1.5f;
    private float _runningSpeed; //must be 4 times higher than speed
    public float animationMaxSpeed = 2f;
    private float _animationSpeed = 0f;
    private float _currentSpeed; //formerly speed
    private float _currentMaxSpeed;
    private const float Acceleration = 2f;

    private Rigidbody _rigidbody;
    private float _angularVelocity;
    private const float TurnSmoothTime = 0.3f;
    private Vector3 _direction;
    private Vector3 _currentTargetPosition;
    private bool _isIdle;
    private int _velocityZHash; //forwards animation
    private int _RifleAimHash;
    private bool _isEngaging;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _currentSpeed = walkingSpeed; //for testing purposes
        _runningSpeed = 4 * walkingSpeed;
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _RifleAimHash = Animator.StringToHash("RifleAim");
        _direction = Vector3.zero;
        _isIdle = true;
        _currentTargetPosition = transform.position;
        _isEngaging = false;
    }

    //called by detection script at start
    public void InitialiseEnemyControllerScript(GameObject target)
    {
        _twoDimensionalAnimationStateController = target.GetComponent<TwoDimensionalAnimationStateController>();
    }

    private void FixedUpdate()
    {
        if (!_isEngaging || !_isIdle)
        {
            if (_currentTargetPosition != transform.position)
            {
                // Continue Patrol
                MoveToCurrentTarget();
            }
        }
        else { }
    }

    private void RotateToCurrentTarget(Vector3 targetDirection)
    {
        float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(
            this.transform.eulerAngles.y,
            targetAngle,
            ref _angularVelocity,
            TurnSmoothTime
        );
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void MoveToCurrentTarget()
    {
        _direction = CalculateDirection(_currentTargetPosition);
        RotateToCurrentTarget(_direction);

        Vector3 currentPosition = transform.position;
        float distanceFromGoal = Vector3.Distance(currentPosition, _currentTargetPosition);
        CalculateCurrentSpeed(distanceFromGoal);
        Vector3 intermediatePosition =
            currentPosition + _direction * (_currentSpeed * Time.fixedDeltaTime);

        //move and transform Enemy
        _rigidbody.MovePosition(intermediatePosition);

        // maps  to animation max speed boundaries
        if (_animator)
        {
            _animationSpeed = (_currentSpeed / _runningSpeed) * animationMaxSpeed;
            _animator.SetFloat(_velocityZHash, _animationSpeed);
        }
    }

    //adjust speed when starting to move or getting close to the goal position
    void CalculateCurrentSpeed(float distanceFromGoal)
    {
        if (_isIdle)
        {
            _currentSpeed = 0f;
        }
        else if (distanceFromGoal < 0.1f)
        {
            _currentSpeed = 0f;
        }
        else if (_currentSpeed > walkingSpeed && distanceFromGoal < _currentSpeed)
        {
            // decelerate from running
            _currentSpeed -= 4f * Time.fixedDeltaTime;
        }
        else if (distanceFromGoal < _currentSpeed / 2)
        {
            //slowly decelerate from walking
            _currentSpeed -= Acceleration * Time.fixedDeltaTime;
        }
        else if (_currentSpeed < _currentMaxSpeed)
        {
            _currentSpeed += Acceleration * Time.fixedDeltaTime;
        }
        else if (_currentSpeed > _currentMaxSpeed)
        {
            _currentSpeed = _currentMaxSpeed;
        }

        if (_currentSpeed < 0f)
        {
            _currentSpeed = 0f;
        }
    }

    public void MoveTo(Vector3 position, bool isRunning)
    {
        //Debug.Log("Guard: New Target to MoveTo");
        _currentTargetPosition = position;
        _currentMaxSpeed = isRunning ? _runningSpeed : walkingSpeed;
        _isIdle = false;
    }

    public void Stop()
    {
        // _currentTargetPosition = transform.position;
        _isIdle = true;
        _currentSpeed = 0f;
    }

    private Vector3 CalculateDirection(Vector3 targetPosition)
    {
        return (targetPosition - transform.position).normalized;
    }

    public void EngageTarget(Vector3 targetDirection)
    {
        if (!_isEngaging)
        {
            Stop();
            _isEngaging = true;
            RotateToCurrentTarget(targetDirection);
            _animator.SetBool(_RifleAimHash, true);
            _twoDimensionalAnimationStateController.GettingEngaged(targetDirection);
        } else
        {
            RotateToCurrentTarget(targetDirection);
        }
    }

    private void DisgageTarget()
    {
        _isEngaging = false;
        _animator.SetBool(_RifleAimHash, false);
        _isIdle = false;
    }

    public void KillTarget()
    {
        _twoDimensionalAnimationStateController.GettingKilled();
    }
}
