using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{

    Animator animator;
    PlayerInput input;
    private CharacterController controller;

    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    public float maximumCrouchVelocity = 0.5f;
    public float maximumProneVelocity = 0.25f;
    public float maximumBackwardsVelocity = 0.5f;
    bool isCrouched = false;
    bool isProne = false;
     
    // hashes
    int VelocityXHash;
    int VelocityZHash;
    int fallDownBackwardsHash;
    int isCrouchedHash;
    int isProneHash;

    //variables to store player input
    bool forwardPressed;
    bool backwardPressed;
    bool runPressed;
    bool leftPressed;
    bool rightPressed;
    bool crouchedClicked;
    bool proneClicked;

    void Awake() 
    {
        input = new PlayerInput();
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementForward.performed += ctx => forwardPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementBackward.performed += ctx => backwardPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementLeft.performed += ctx => leftPressed = ctx.ReadValueAsButton();
        input.CharacterControls.MovementRight.performed += ctx => rightPressed = ctx.ReadValueAsButton();
        input.CharacterControls.Crouch.started += ctx => crouchedClicked = ctx.ReadValueAsButton();
        input.CharacterControls.Prone.started += ctx => proneClicked = ctx.ReadValueAsButton();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
        fallDownBackwardsHash = Animator.StringToHash("fallDownBackwards");
        isCrouchedHash = Animator.StringToHash("isCrouched");
        isProneHash = Animator.StringToHash("isProne");
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
        // trying to sprint backwards while standing results in the character stumbeling
        if (backwardPressed && runPressed && !isCrouched && !isProne)
        {
            velocityZ = 0f;
            velocityX = 0f;
            animator.SetBool(fallDownBackwardsHash, true);
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
        else if (backwardPressed && velocityZ > -maximumBackwardsVelocity && velocityZ < (-maximumBackwardsVelocity + 0.05f))
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
    // changes stance if there is some input that motivates such a change
    // prone overrides crouch
    // character stands up when runPressed ist held.
    void changeStance(bool runPressed, bool crouchedClicked, bool proneClicked)
    {
        if (runPressed && (isCrouched || isProne))
        {
            isCrouched = false;
            isProne = false;
            changeControllerCollider();
        } else if (proneClicked)
        {
            isProne = !isProne;
            changeControllerCollider();
        } else if (crouchedClicked)
        {
            isCrouched = !isCrouched;
            changeControllerCollider();
        } 
    }

    float calculateCurrentMaxVelocity(bool runPressed) 
    {
        float currentMaxVelocity;
        if (isProne)
        {
            currentMaxVelocity = maximumProneVelocity;
        } else if (isCrouched)
        {
            currentMaxVelocity = maximumCrouchVelocity;
        } else 
        {
            currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        }
        return currentMaxVelocity;
    }

    void changeControllerCollider()
    {
        if (isProne)
        {
            controller.height = 0.25f;
            controller.radius = 0.33f; 
            controller.center = new Vector3(0.1f, 0.39f, 0.2f);
        } else if (isCrouched)
        {
            controller.height = 1.0f;
            controller.radius = 0.3f; 
            controller.center = new Vector3(0f, 0.56f, 0.2f);
        } else {
        controller.height = 1.72f;
        controller.radius = 0.2f; 
        controller.center = new Vector3(0f, 0.92f, 0f);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
                // animator.SetBool("fallDownBackwards", false); 
        changeStance(runPressed, crouchedClicked, proneClicked);
        float currentMaxVelocity = calculateCurrentMaxVelocity(runPressed);

        changeVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        
        //reset clicked values
        crouchedClicked = false;
        proneClicked = false;
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX); 
        animator.SetBool(isCrouchedHash, isCrouched);
        animator.SetBool(isProneHash, isProne);
        
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
