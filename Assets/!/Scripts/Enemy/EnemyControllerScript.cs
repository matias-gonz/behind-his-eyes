using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    // Components
    private Animator _animator;
    private PlayerController _playerController;
    private Rigidbody _rigidbody;

    // Enemy Movement and Animation
    public float walkingSpeed = 1.5f;
    private float _runningSpeed; //must be 4 times higher than speed
    public float animationMaxSpeed = 2f;
    private float _animationSpeed = 0f;
    private float _currentSpeed; //formerly speed
    private float _currentMaxSpeed;
    private const float Acceleration = 2f;
    private const float TurnSmoothTime = 0.2f;
    private float _angularVelocity;

    //Target
    private Vector3 _targetDirection;
    private Vector3 _currentTargetPosition;

    // Guard Status
    private bool _isEngaging;
    private bool _patrolWait;
    private bool _isGuarding;

    // Hashes
    private int _velocityZHash; //forwards animation
    private int _rifleAimHash;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _currentSpeed = walkingSpeed; //for testing purposes
        _runningSpeed = 4 * walkingSpeed;
        _targetDirection = Vector3.zero;
        _currentTargetPosition = transform.position;
        _isEngaging = false;
        _patrolWait = true;
        _isGuarding = true;
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _rifleAimHash = Animator.StringToHash("RifleAim");
    }

    //called by detection script at start
    public void InitialiseEnemyControllerScript(GameObject target)
    {
        _playerController =
            target.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!_isEngaging && !_isGuarding)
        {
            // Continue Patrol
            MoveToCurrentTarget();
        }
        else if (_isEngaging)
        {
            RotateToCurrentTarget(_targetDirection);
        }
    }

    // Patrolling methods
    public void PatrolTo(Vector3 position, bool isRunning)
    {
        if (_isEngaging) return;
        
        _currentTargetPosition = position;
        _currentMaxSpeed = isRunning ? _runningSpeed : walkingSpeed;
        _patrolWait = false;
        _isGuarding = false;
    }

    public void PatrolWait()
    {
        if (_isEngaging) return;
        
        _patrolWait = true;
        _currentSpeed = 0f;
    }

    // Engaging Target Methods
    public void EngageTarget(Vector3 targetDirection)
    {
        if (!_isEngaging)
        {
            PatrolWait();
            _isEngaging = true;
            _targetDirection = targetDirection;
            _animator.SetBool(_rifleAimHash, true);
            _playerController.GettingSpotted();
        }
        else
        {
            _targetDirection = targetDirection;
        }
    }

    public void KillTarget()
    {
        _playerController.GettingKilled(_targetDirection);
    }

    private void DisengageTarget()
    {
        _isEngaging = false;
        _animator.SetBool(_rifleAimHash, false);
        _patrolWait = false;
    }

    // Enemy movement
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
        Vector3 currentPosition = transform.position;
        float distanceFromGoal = Vector3.Distance(currentPosition, _currentTargetPosition);
        //always calculate so animator can adjust speed
        CalculateCurrentSpeed(distanceFromGoal);
        if (distanceFromGoal >= 0.1f)
        {
            Vector3 direction = CalculateDirection(_currentTargetPosition);
            RotateToCurrentTarget(direction);
            Vector3 intermediatePosition =
                currentPosition + direction * (_currentSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(intermediatePosition);
        }

        // maps  to animation max speed boundaries
        if (_animator)
        {
            _animationSpeed = (_currentSpeed / _runningSpeed) * animationMaxSpeed;
            _animator.SetFloat(_velocityZHash, _animationSpeed);
        }
    }

    private Vector3 CalculateDirection(Vector3 targetPosition)
    {
        return (targetPosition - transform.position).normalized;
    }

    //adjust speed when starting to move or getting close to the goal position
    private void CalculateCurrentSpeed(float distanceFromGoal)
    {
        if (_patrolWait)
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
}
