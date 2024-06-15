using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    Animator animator;
    public float walkingSpeed = 1.5f;
    float runningSpeed; //must be 4 times higher than speed
    public float animationMaxSpeed = 2f;
    float animationSpeed = 0f;
    float currentSpeed; //formerly speed
    float currentMaxSpeed;
    float acceleration = 2f;
    

    private Rigidbody _rigidbody;
    private float _angularVelocity;
    private float _turnSmoothTime = 0.3f;
    private Vector3 _direction;
    private Vector3 currentTargetPosition;
    int VelocityXHash; //sidewards animation, can be use for strafe walking
    int VelocityZHash; //forwards animation

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();  
        currentSpeed = walkingSpeed; //for testing purposes
        runningSpeed = 4 * walkingSpeed;
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
     
    }

    private void FixedUpdate()
    {
        calculateDirection();
        // if (_direction == Vector3.zero)
        // {
        //     return;
        // }        
        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _angularVelocity,
            _turnSmoothTime);
        Vector3 currentPosition = transform.position;
        //adjust speed when starting to move or getting close to the goal position
        float distanceFromGoal = Vector3.Distance(currentPosition, currentTargetPosition);
        if (distanceFromGoal < currentSpeed )
        {
            // currentSpeed = distanceFromGoal*4;
            currentSpeed -= acceleration * Time.fixedDeltaTime;
        } else if (currentSpeed < currentMaxSpeed)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        } else if (currentSpeed > currentMaxSpeed)
        {
            currentSpeed = currentMaxSpeed;
        }
        if (currentSpeed < 0f)
        {
            currentSpeed = 0f;
        }
        Vector3 intermediatePosition = currentPosition + _direction * (currentSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(intermediatePosition);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        // maps  to animation max speed boundaries
        animationSpeed = (currentSpeed/runningSpeed) * animationMaxSpeed; 
        animator.SetFloat(VelocityZHash, animationSpeed);
    }

    public void MoveTo(Vector3 position, bool isRunning)
    {
        Debug.Log("Guard: New Target to MoveTo");
        currentTargetPosition = position;
        currentMaxSpeed = isRunning ? runningSpeed : walkingSpeed;
    }

    public void Stop()
    {
        // currentSpeed = 0f;
        currentTargetPosition = transform.position;
    }

    private void calculateDirection()
    {
        Vector3 direction = (currentTargetPosition - transform.position).normalized;
        _direction = direction;
    }
}
