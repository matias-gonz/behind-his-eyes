using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    Animator animator;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 6f; //must be 4 times higher than speed
    public float animationMaxSpeed = 2f;
    float animationSpeed = 0f;
    float currentSpeed = 4f; //formerly speed
    

    private Rigidbody _rigidbody;
    private float _angularVelocity;
    private float _turnSmoothTime = 0.1f;
    private Vector3 _direction;
    int VelocityXHash; //sidewards animation, can be use for strafe walking
    int VelocityZHash; //forwards animation

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();  
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
        currentSpeed = walkingSpeed; //for testing purposes
     
    }

    private void FixedUpdate()
    {
        if (_direction == Vector3.zero)
        {
            return;
        }

        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _angularVelocity,
            _turnSmoothTime);
        _rigidbody.MovePosition(transform.position + _direction * (currentSpeed * Time.fixedDeltaTime));
        transform.rotation = Quaternion.Euler(0, angle, 0);
        // maps current Speed to animation speed boundaries
        animationSpeed = (currentSpeed/runningSpeed) * animationMaxSpeed; 
        animator.SetFloat(VelocityZHash, animationSpeed);
    }

    public void MoveTo(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Move(direction);
    }

    public void Stop()
    {
        Move(Vector3.zero);
    }

    private void Move(Vector3 direction)
    {
        _direction = direction;
    }
}
