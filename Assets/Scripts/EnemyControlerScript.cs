using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody _rigidbody;
    private float _angularVelocity;
    private float _turnSmoothTime = 0.1f;
    private Vector3 _direction;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        _rigidbody.MovePosition(transform.position + _direction * (speed * Time.fixedDeltaTime));
        transform.rotation = Quaternion.Euler(0, angle, 0);
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