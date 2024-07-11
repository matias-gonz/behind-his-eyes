using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ThirdPersonMovement _thirdPersonMovement;
    public TwoDimensionalAnimationStateController _animationController;
    private PlayerInput _input;

    //variables to store player input
    private bool _forwardPressed;
    private bool _backwardPressed;
    private bool _runPressed;
    private bool _leftPressed;
    private bool _rightPressed;
    private bool _crouchedClicked;
    private bool _proneClicked;
    private bool _jumpPressed;

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
            StancePressed();
        }

        _animationController.UpdateVelocity(
            _forwardPressed,
            _backwardPressed,
            _leftPressed,
            _rightPressed,
            _runPressed
        );
        _crouchedClicked = false;
        _proneClicked = false;
    }

    private bool AirBorne()
    {
        if (_animationController.IsJumping())
            return true;
        return !_thirdPersonMovement.GetIsGrounded();
    }

    private void JumpPressed(bool isStanding)
    {
        if (!_jumpPressed)
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
        if (_runPressed || _jumpPressed)
            return;
        if (_proneClicked)
        {
            _animationController.ToggleProne();
        }
        else if (_crouchedClicked)
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
