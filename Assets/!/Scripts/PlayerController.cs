using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;
    public TwoDimensionalAnimationStateController animationController;
    public CameraControl _cameraControl;
    private PlayerInput _input;

    //detection parameters
    public float goIntoProneTime = 0.8f;
    public float standingViewFactor = 1f;
    public float crouchingViewFactor = 0.8f;
    public float proneViewFactor = 0.65f;
    public float jumpingSoundFactor = 1f;
    public float standingSoundFactor = 2f;
    public float crouchingSoundFactor = 1.5f;
    public float proneSoundFactor = 2.5f;

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
    private bool _rifleAimClicked = false;
    private bool _rifleFireClicked = false;

    // Rifle Aim Down Sights parameters
    public bool _aimMode = false;
    private bool _inAimZone = false;

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
        _input.CharacterControls.RifleAim.started += ctx =>
            _rifleAimClicked = ctx.ReadValueAsButton();
        _input.CharacterControls.RifleFire.started += ctx =>
            _rifleFireClicked = ctx.ReadValueAsButton();
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
        if (_aimMode)
        {
            RifleAction();
            _rifleFireClicked = false;
            return;
        }
        bool isAirBorne = AirBorne();
        // block playerinput if animation is currently being played
        if (animationController.CheckAnimationEventPlaying())
            return;
        if (!isAirBorne)
        {
            bool isStanding = animationController.IsStanding();
            JumpPressed(isStanding);
            RunPressed(isStanding);
            CrouchPronePressed();
            StancePressed();
            if (_inAimZone)
            {
                GoAimMode(isStanding);
            }
        }

        animationController.UpdateVelocity(
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
        _rifleAimClicked = false;
        _rifleFireClicked = false;
    }

    public float GetViewDistance(float maximumViewDistance)
    {
        float viewFactor = 1f;
        if (animationController.IsStanding())
        {
            viewFactor = standingViewFactor;
        }
        else if (animationController.IsCrouched())
        {
            viewFactor = crouchingViewFactor;
        }
        else if (animationController.IsProne())
        {
            viewFactor = proneViewFactor;
        }
        return viewFactor * maximumViewDistance;
    }

    public float GetNoiseDistance(float maximumNoiseDistance)
    {
        float soundFactor = 1f;
        if (animationController.IsJumping())
        {
            return jumpingSoundFactor * maximumNoiseDistance;
        }
        if (animationController.IsStanding())
        {
            soundFactor = standingSoundFactor;
        }
        else if (animationController.IsCrouched())
        {
            soundFactor = crouchingSoundFactor;
        }
        else if (animationController.IsProne())
        {
            soundFactor = proneSoundFactor;
        }
        return soundFactor * thirdPersonMovement.SpeedNoiseFactor() * maximumNoiseDistance;
    }

    public void GettingKilled(Vector3 direction)
    {
        if (!GameManager.Instance.godMode)
        {
            animationController.Dying();
            _cameraControl.RotateTPCam(direction);
        }
    }

    public void InAimZone(bool isInAimZone)
    {
        _inAimZone = isInAimZone;
    }

    private void GoAimMode(bool isStanding)
    {
        if (!_rifleAimClicked)
            return;
        if (!isStanding)
        {
            animationController.StandUp();
        }
        _aimMode = true;
        thirdPersonMovement.AimModeTransform();
        animationController.RifleAim();
    }

    private void RifleAction()
    {
        Vector2 mouse = _input.CharacterControls.Look.ReadValue<Vector2>();
        thirdPersonMovement.RifleAim(mouse.x, mouse.y);
        if (_rifleFireClicked)
        {
            animationController.RifleFire();
        }
    }

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
        if (animationController.IsJumping())
            return true;
        return !thirdPersonMovement.GetIsGrounded();
    }

    private void JumpPressed(bool isStanding)
    {
        if (!_jumpClicked)
            return;
        if (isStanding)
        {
            animationController.StartToJump();
        }
        else
        {
            animationController.StandUp();
        }
    }

    private void RunPressed(bool isStanding)
    {
        if (!_runPressed)
            return;
        if (isStanding)
            return;
        animationController.StandUp();
    }

    private void StancePressed()
    {
        if (_runPressed || _jumpClicked)
            return;
        if (_pronePressed)
        {
            animationController.ToggleProne();
        }
        else if (_crouchedPressed)
        {
            animationController.ToggleCrouch();
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
