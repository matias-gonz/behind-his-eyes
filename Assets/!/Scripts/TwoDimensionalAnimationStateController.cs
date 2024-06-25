using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    //constants
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    public float maximumCrouchVelocity = 0.5f;
    public float maximumProneVelocity = 0.25f;
    public float maximumBackwardsVelocity = 0.5f;

    // references
    private Animator _animator;
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    public Collider standingCollider;
    public Collider crouchCollider;
    public Collider proneCollider;

    // local variables for animation state
    private bool _isCrouched = false;
    private bool _isProne = false;
    private float _velocityX = 0.0f;
    private float _velocityZ = 0.0f;

    // hashes
    private int _velocityXHash;
    private int _velocityZHash;
    private int _fallDownBackwardsHash;
    private int _isCrouchedHash;
    private int _isProneHash;
    private int _isJumpHash;

    //variables to store player input
    private bool _forwardPressed;
    private bool _backwardPressed;
    private bool _runPressed;
    private bool _leftPressed;
    private bool _rightPressed;
    private bool _crouchedClicked;
    private bool _proneClicked;
    private bool _jumpPressed;
    private bool _allowPlayerInput = true;

    void Awake()
    {
        _input = new PlayerInput();
        _input.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.MovementForward.performed += ctx =>
            _forwardPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.MovementBackward.performed += ctx =>
            _backwardPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.MovementLeft.performed += ctx =>
            _leftPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.MovementRight.performed += ctx =>
            _rightPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.Crouch.started += ctx =>
            _crouchedClicked = ctx.ReadValueAsButton();
        _input.CharacterControls.Prone.started += ctx => _proneClicked = ctx.ReadValueAsButton();
        _input.CharacterControls.Jump.performed += ctx => _jumpPressed = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _fallDownBackwardsHash = Animator.StringToHash("fallDownBackwards");
        _isCrouchedHash = Animator.StringToHash("isCrouched");
        _isProneHash = Animator.StringToHash("isProne");
        _isJumpHash = Animator.StringToHash("isJump");
    }

    void ChangeVelocity(
        bool forwardPressed,
        bool backwardPressed,
        bool leftPressed,
        bool rightPressed,
        bool runPressed,
        float currentMaxVelocity
    )
    {
        // accelerate forward
        if (forwardPressed && _velocityZ < currentMaxVelocity)
        {
            _velocityZ += Time.deltaTime * acceleration;
        }

        // accelerate backward, but only to walking speed
        if (backwardPressed && _velocityZ > -maximumBackwardsVelocity)
        {
            _velocityZ -= Time.deltaTime * acceleration;
        }

        //accelerate into left strafe
        if (leftPressed && _velocityX > -currentMaxVelocity)
        {
            _velocityX -= Time.deltaTime * acceleration;
        }

        //accelerate into right strafe
        if (rightPressed && _velocityX < currentMaxVelocity)
        {
            _velocityX += Time.deltaTime * acceleration;
        }

        //decelerate up to halt from forward
        if (!forwardPressed && _velocityZ > 0.0f)
        {
            _velocityZ -= Time.deltaTime * deceleration;
        }

        //decelerate up to halt from backward
        if (!backwardPressed && _velocityZ < 0.0f)
        {
            _velocityZ += Time.deltaTime * deceleration;
        }

        //decelerate from left strafe
        if (!leftPressed && _velocityX < 0.0f)
        {
            _velocityX += Time.deltaTime * deceleration;
        }

        //decelerate from right strafe
        if (!rightPressed && _velocityX > 0.0f)
        {
            _velocityX -= Time.deltaTime * deceleration;
        }

        // trying to sprint backwards while standing results in the character stumbeling
        if (backwardPressed && runPressed && !_isCrouched && !_isProne)
        {
            _velocityZ = 0f;
            _velocityX = 0f;
            _animator.SetBool(_fallDownBackwardsHash, true);
        }
    }

    void LockOrResetVelocity(
        bool forwardPressed,
        bool backwardPressed,
        bool leftPressed,
        bool rightPressed,
        bool runPressed,
        float currentMaxVelocity
    )
    {
        //reset velocityZ
        if (!forwardPressed && !backwardPressed && (_velocityZ > -0.05f && _velocityZ < 0.05f))
        {
            _velocityZ = 0.0f;
        }

        //reset velocityX
        if (
            !leftPressed
            && !rightPressed
            && _velocityX != 0.0f
            && (_velocityX > -0.05f && _velocityX < 0.05f)
        )
        {
            _velocityX = 0.0f;
        }

        //lock forward
        if (forwardPressed && runPressed && _velocityZ > currentMaxVelocity)
        {
            _velocityZ = currentMaxVelocity;
        }
        //decelerate to maxiumum walk velocity
        else if (forwardPressed && _velocityZ > currentMaxVelocity)
        {
            _velocityZ -= Time.deltaTime * deceleration;
            //round currentMaxVelocity if within offset
            if (_velocityZ > currentMaxVelocity && _velocityZ < (currentMaxVelocity + 0.05))
            {
                _velocityZ = currentMaxVelocity;
            }
        }
        // round to currentMaxVelocity if within offset
        else if (
            forwardPressed
            && _velocityZ < currentMaxVelocity
            && _velocityZ > (currentMaxVelocity - 0.05f)
        )
        {
            _velocityZ = currentMaxVelocity;
        }

        //lock backward to walk speed
        if (backwardPressed && _velocityZ < -maximumBackwardsVelocity)
        {
            _velocityZ = -maximumBackwardsVelocity;
        }
        // round to maximumWalkVelocity if within offset
        else if (
            backwardPressed
            && _velocityZ > -maximumBackwardsVelocity
            && _velocityZ < (-maximumBackwardsVelocity + 0.05f)
        )
        {
            _velocityZ = -maximumBackwardsVelocity;
        }

        //locking left
        if (leftPressed && runPressed && _velocityX < -currentMaxVelocity)
        {
            _velocityX = -currentMaxVelocity;
        }
        // decelerate to the maximum walk velocity
        else if (leftPressed && _velocityX < -currentMaxVelocity)
        {
            _velocityX += Time.deltaTime * deceleration;
            //round the currentMaxVelocity within offset
            if (_velocityX < -currentMaxVelocity && _velocityX > (-currentMaxVelocity - 0.05f))
            {
                _velocityX = -currentMaxVelocity;
            }
        }
        //round the currentMaxVelocity within offset
        else if (
            leftPressed
            && _velocityX > -currentMaxVelocity
            && _velocityX < (-currentMaxVelocity + 0.05f)
        )
        {
            _velocityX = -currentMaxVelocity;
        }

        //locking right
        if (rightPressed && runPressed && _velocityX > currentMaxVelocity)
        {
            _velocityX = currentMaxVelocity;
        }
        // decelerate to the maximum walk velocity
        else if (rightPressed && _velocityX > currentMaxVelocity)
        {
            _velocityX -= Time.deltaTime * deceleration;
            //round the currentMaxVelocity within offset
            if (_velocityX > currentMaxVelocity && _velocityX < (currentMaxVelocity + 0.05f))
            {
                _velocityX = currentMaxVelocity;
            }
        }
        //round the currentMaxVelocity within offset
        else if (
            rightPressed
            && _velocityX < currentMaxVelocity
            && _velocityX > (currentMaxVelocity - 0.05f)
        )
        {
            _velocityX = currentMaxVelocity;
        }
    }

    // changes stance if there is some input that motivates such a change
    // prone overrides crouch
    // character stands up when runPressed ist held.
    void ChangeStance(bool runPressed, bool crouchedClicked, bool proneClicked)
    {
        if (runPressed && (_isCrouched || _isProne))
        {
            _isCrouched = false;
            _isProne = false;
            ChangeControllerCollider();
        }
        else if (proneClicked)
        {
            _isProne = !_isProne;
            ChangeControllerCollider();
        }
        else if (crouchedClicked)
        {
            _isCrouched = !_isCrouched;
            ChangeControllerCollider();
        }
        // only call ChangeControllerCollider when stance not changed
    }

    float CalculateCurrentMaxVelocity(bool runPressed)
    {
        float currentMaxVelocity;
        if (_isProne)
        {
            currentMaxVelocity = maximumProneVelocity;
        }
        else if (_isCrouched)
        {
            currentMaxVelocity = maximumCrouchVelocity;
        }
        else
        {
            currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        }

        return currentMaxVelocity;
    }

    void ChangeControllerCollider()
    {
        if (_isProne)
        {
            proneCollider.enabled = true;
            crouchCollider.enabled = false;
            standingCollider.enabled = false;
        }
        else if (_isCrouched)
        {
            proneCollider.enabled = false;
            crouchCollider.enabled = true;
            standingCollider.enabled = false;
        }
        else
        {
            proneCollider.enabled = false;
            crouchCollider.enabled = false;
            standingCollider.enabled = true;
        }
    }

    void CheckPlayerInputAllowed()
    {
        bool animationEventPlaying = _animator.GetBool(_fallDownBackwardsHash);
        // block playerinput if animation is currently being played
        _allowPlayerInput = !animationEventPlaying;
    }

    // jumping can be triggered while player is standing and not in a Jump
    void StartToJump()
    {
        bool isJump = _animator.GetBool(_isJumpHash);
        if (_jumpPressed && !isJump && !_isProne && !_isCrouched)
        {
            // GetComponent<ThirdPersonMovement>().StopJump();
            // _rigidbody.useGravity = false;
            _animator.SetBool(_isJumpHash, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartToJump();
        CheckPlayerInputAllowed();
        if (_allowPlayerInput)
        {
            // handle player input
            ChangeStance(_runPressed, _crouchedClicked, _proneClicked);
            float currentMaxVelocity = CalculateCurrentMaxVelocity(_runPressed);

            ChangeVelocity(
                _forwardPressed,
                _backwardPressed,
                _leftPressed,
                _rightPressed,
                _runPressed,
                currentMaxVelocity
            );
            LockOrResetVelocity(
                _forwardPressed,
                _backwardPressed,
                _leftPressed,
                _rightPressed,
                _runPressed,
                currentMaxVelocity
            );
        }

        //reset clicked values
        _crouchedClicked = false;
        _proneClicked = false;
        _animator.SetFloat(_velocityZHash, _velocityZ);
        _animator.SetFloat(_velocityXHash, _velocityX);
        _animator.SetBool(_isCrouchedHash, _isCrouched);
        _animator.SetBool(_isProneHash, _isProne);
    }

    // Toggle character controls action map
    void OnEnable()
    {
        _input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _input.CharacterControls.Disable();
    }
}
