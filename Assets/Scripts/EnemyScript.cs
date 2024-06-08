using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody _rigidbody;
    private float _angularVelocity;
    private float _turnSmoothTime = 0.1f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -1).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _angularVelocity, _turnSmoothTime);
        _rigidbody.MovePosition(this.transform.position + direction * (speed * Time.fixedDeltaTime));
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}