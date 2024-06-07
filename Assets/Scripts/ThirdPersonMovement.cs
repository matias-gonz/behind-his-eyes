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
        Vector3 direction = new Vector3(sidewardMovement,0f,forwardMovement);

        float targetAngle = cam.eulerAngles.y; //Mathf.Atan2(velocityZ, velocityX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction;//Vector3.forward;
        controller.Move(moveDir * 2 * Time.deltaTime);
        Debug.Log(moveDir);
    }
}
