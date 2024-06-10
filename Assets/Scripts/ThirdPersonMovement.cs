using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    Animator animator;
    int VelocityXHash;
    int VelocityZHash;
    float forwardMovement;
    float sidewardMovement;
    public CharacterController controller;
    public Transform cam;
    //float turnSpeed = 90f; // degrees per second
    public float jumpSpeed = 4f;
    public float gravity = 9.8f;
    private float vSpeed = 0f; // current vertical velocity

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
    }

    // Update is called once per frame
    void Update()
    {
        forwardMovement = animator.GetFloat(VelocityZHash);
        sidewardMovement = animator.GetFloat(VelocityXHash);
        // walk direction in normal cordinate system
        Vector3 direction = new Vector3(sidewardMovement, 0f, forwardMovement);

        float targetAngle = cam.eulerAngles.y; //Mathf.Atan2(velocityZ, velocityX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        // make sure character moves in direction of target angle
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction; //Vector3.forward;
        controller.Move(moveDir * 2 * Time.deltaTime);

        if (controller.isGrounded)
        {
            vSpeed = 0; // grounded character has vSpeed = 0...
            if (Input.GetKeyDown("space"))
            { // unless it jumps:
                vSpeed = jumpSpeed;
            }
        } else
        {
             vSpeed -= gravity * Time.deltaTime;
        }
        // apply gravity acceleration to vertical speed:
       
        Vector3 vertMove = new Vector3(0f, vSpeed, 0f);
        controller.Move(vertMove * Time.deltaTime);
    
    }
}
