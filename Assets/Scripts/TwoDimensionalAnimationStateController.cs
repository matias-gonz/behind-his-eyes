using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{

    Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    public float maximumBackwardsVelocity = 0.25f;
    int VelocityXHash;
    int VelocityZHash;

    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    //variables to store player input
    bool forwardPressed;
    bool backwardPressed;
    bool runPressed;
    bool leftPressed;
    bool rightPressed;

    void Awake() 
    {
        input = new PlayerInput();
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementForward.performed += ctx => forwardPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementBackward.performed += ctx => backwardPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementLeft.performed += ctx => leftPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementRight.performed += ctx => rightPressed = ctx.ReadValueAsButton();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }


    void changeVelocity(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // accelerate forward
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ+= Time.deltaTime * acceleration;
        }
        // accelerate backward, but only to walking speed
        if (backwardPressed && velocityZ > -maximumBackwardsVelocity)
        {
            velocityZ-= Time.deltaTime * acceleration;
        }
         //accelerate into left strafe
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX-= Time.deltaTime * acceleration;
        }
        //accelerate into right strafe
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX+= Time.deltaTime * acceleration;
        }
        //decelerate up to halt from forward
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ-= Time.deltaTime * deceleration;
        }
        //decelerate up to halt from backward
        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ+= Time.deltaTime * deceleration;
        }
         //decelerate from left strafe
        if (!leftPressed &&  velocityX < 0.0f)
        {
            velocityX+= Time.deltaTime * deceleration;
        }
        //decelerate from right strafe
        if ( !rightPressed && velocityX > 0.0f)
        {
            velocityX-= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        //reset velocityZ
        if (!forwardPressed && !backwardPressed && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }
        //reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        //lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        //decelerate to maxiumum walk velocity
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            //round currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ <(currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        // round to currentMaxVelocity if within offset
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        //lock backward to walk speed
        if (backwardPressed && velocityZ < -maximumBackwardsVelocity)
        {
            velocityZ = -maximumBackwardsVelocity;
        }
        // round to maximumWalkVelocity if within offset
        else if (backwardPressed && velocityZ > -maximumBackwardsVelocity && velocityZ < (maximumBackwardsVelocity + 0.05f))
        {
            velocityZ = -maximumBackwardsVelocity;
        }

        //locking left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        // decelerate to the maximum walk velocity 
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            //round the currentMaxVelocity within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity -0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        //round the currentMaxVelocity within offset
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        //locking right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        // decelerate to the maximum walk velocity 
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            //round the currentMaxVelocity within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity +0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        //round the currentMaxVelocity within offset
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        changeVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);  
        Debug.Log(velocityZ);    
    }

    // Toggle character controls action map
    void OnEnable()
    {
        input.CharacterControls.Enable();
    }
    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}
