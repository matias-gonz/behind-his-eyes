using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ThirdPersonMovement _thirdPersonMovement;
    public TwoDimensionalAnimationStateController _animationController;
    private PlayerInput _input;

    public float goIntoProneTime = 0.8f;
    public float _standingViewFactor = 1f;
    public float _crouchingViewFactor = 0.5f;
    public float _proneViewFactor = 0.3f;

    //variables to store player input
    private bool _forwardPressed;
    private bool _backwardPressed;
    private bool _runPressed;
    private bool _leftPressed;
    private bool _rightPressed;
    private bool _crouchPronePerformed;
    private bool _crouchProneCanceled = false;
    private float _crouchedProneTimer = 0f;
    private bool _crouchedPressed;
    private bool _pronePressed;
    private bool _jumpClicked = false;

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
        _input.CharacterControls.CrouchProne.performed += ctx =>
            _crouchPronePerformed = ctx.ReadValueAsButton();
        _input.CharacterControls.CrouchProne.canceled += ctx => _crouchProneCanceled = true;

        _input.CharacterControls.Jump.started += ctx => _jumpClicked = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        // turns mouse cursor invisible and locks it in place, allowing indefinite mouse movement
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAirBorne = AirBorne();
        // block playerinput if animation is currently being played
        if (_animationController.CheckAnimationEventPlaying())
            return;
        if (!isAirBorne)
        {
            bool isStanding = _animationController.IsStanding();
            JumpPressed(isStanding);
            RunPressed(isStanding);
            CrouchPronePressed();
            StancePressed();
        }

        _animationController.UpdateVelocity(
            _forwardPressed,
            _backwardPressed,
            _leftPressed,
            _rightPressed,
            _runPressed
        );
        _crouchedPressed = false;
        _pronePressed = false;
        _crouchProneCanceled = false;
        _jumpClicked = false;
    }
    
    public float GetViewDistance(float maximumViewDistance)
    {
        float viewDistance;
        if (_animationController.IsStanding()) {
            viewDistance = _standingViewFactor * maximumViewDistance;
        } else
        {
            viewDistance = maximumViewDistance;
        }
        return viewDistance;

    }

    // public float GetNoiseDistance()
    // {

    // }

    private void CrouchPronePressed()
    {
        if (_crouchProneCanceled)
        {
            if (_crouchedProneTimer < goIntoProneTime)
            {
                _crouchedPressed = true;
            }
            else
            {
                _crouchedProneTimer = 0f;
            }
        }

        if (_crouchPronePerformed)
        {
            float newTimer = _crouchedProneTimer + Time.deltaTime;
            if (_crouchedProneTimer < goIntoProneTime && newTimer >= goIntoProneTime)
            {
                _pronePressed = true;
            }
            _crouchedProneTimer = newTimer;
        }
    }

    private bool AirBorne()
    {
        if (_animationController.IsJumping())
            return true;
        return !_thirdPersonMovement.GetIsGrounded();
    }

    private void JumpPressed(bool isStanding)
    {
        if (!_jumpClicked)
            return;
        if (isStanding)
        {
            _animationController.StartToJump();
        }
        else
        {
            _animationController.StandUp();
        }
    }

    private void RunPressed(bool isStanding)
    {
        if (!_runPressed)
            return;
        if (isStanding)
            return;
        _animationController.StandUp();
    }

    private void StancePressed()
    {
        if (_runPressed || _jumpClicked)
            return;
        if (_pronePressed)
        {
            _animationController.ToggleProne();
        }
        else if (_crouchedPressed)
        {
            _animationController.ToggleCrouch();
        }
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
