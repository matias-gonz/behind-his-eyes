using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    // references
    private Animator _animator;

    //constants
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    public float maximumCrouchVelocity = 0.5f;
    public float maximumProneVelocity = 0.25f;

    public Collider upRightCollider;
    public Collider proneCollider;

    // local variables for animation state

    private float _velocityX = 0.0f;
    private float _velocityZ = 0.0f;

    // hashes
    private int _velocityXHash;
    private int _velocityZHash;
    private int _fallDownBackwardsHash;
    private int _isCrouchedHash;
    private int _isProneHash;
    private int _isJumpHash;
    private int _DyingHash;
    private int _SpottedHash;
    private int _RifleIdleHash;
    private int _RifleAimHash;
    private int _RifleFireHash;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _fallDownBackwardsHash = Animator.StringToHash("fallDownBackwards");
        _isCrouchedHash = Animator.StringToHash("isCrouched");
        _isProneHash = Animator.StringToHash("isProne");
        _isJumpHash = Animator.StringToHash("isJump");
        _DyingHash = Animator.StringToHash("Dying");
        _SpottedHash = Animator.StringToHash("Spotted");
        _RifleIdleHash = Animator.StringToHash("RifleIdle");
        _RifleAimHash = Animator.StringToHash("RifleAim");
        _RifleFireHash = Animator.StringToHash("RifleFire");
    }

    // Update is called once per frame
    void Update() { }

    public bool CheckAnimationEventPlaying()
    {
        bool animationEventPlaying =
            _animator.GetBool(_fallDownBackwardsHash) || _animator.GetBool(_DyingHash);
        return animationEventPlaying;
    }

    public void RifleAim()
    {
        _animator.SetBool(_RifleIdleHash, true);
        _animator.SetBool(_RifleAimHash, true);
    }

    public void RifleFire()
    {
        if (_animator.GetBool(_RifleAimHash))
        {
            _animator.SetBool(_RifleFireHash, true);
        }
    }

    public bool IsJumping()
    {
        return _animator.GetBool(_isJumpHash);
    }

    public float GetMaximumRunVelocity()
    {
        return maximumRunVelocity;
    }

    // jumping can be triggered while player is standing and not in a Jump
    public void StartToJump()
    {
        _animator.SetBool(_isJumpHash, true);
    }

    // Stance change
    public bool IsStanding()
    {
        return !_animator.GetBool(_isCrouchedHash) && !_animator.GetBool(_isProneHash);
    }

    public bool IsCrouched()
    {
        return _animator.GetBool(_isCrouchedHash);
    }

    public bool IsProne()
    {
        return _animator.GetBool(_isProneHash);
    }

    public void StandUp()
    {
        _animator.SetBool(_isCrouchedHash, false);
        _animator.SetBool(_isProneHash, false);
        ChangeControllerCollider(false);
    }

    public void ToggleCrouch()
    {
        _animator.SetBool(_isCrouchedHash, !_animator.GetBool(_isCrouchedHash));
        _animator.SetBool(_isProneHash, false);
        ChangeControllerCollider(false);
    }

    public void ToggleProne()
    {
        bool goIntoProne = !_animator.GetBool(_isProneHash);
        _animator.SetBool(_isCrouchedHash, false);
        _animator.SetBool(_isProneHash, goIntoProne);
        ChangeControllerCollider(goIntoProne);
    }

    void ChangeControllerCollider(bool isProne)
    {
        if (isProne)
        {
            proneCollider.enabled = true;
            upRightCollider.enabled = false;
        }
        else
        {
            proneCollider.enabled = false;
            upRightCollider.enabled = true;
        }
    }

    public void UpdateVelocity(
        bool forwardPressed,
        bool backwardPressed,
        bool leftPressed,
        bool rightPressed,
        bool runPressed
    )
    {
        float currentMaxVelocity = CalculateCurrentMaxVelocity(runPressed);
        ChangeVelocity(
            forwardPressed,
            backwardPressed,
            leftPressed,
            rightPressed,
            runPressed,
            currentMaxVelocity
        );
        LockOrResetVelocity(
            forwardPressed,
            backwardPressed,
            leftPressed,
            rightPressed,
            runPressed,
            currentMaxVelocity
        );
        _animator.SetFloat(_velocityZHash, _velocityZ);
        _animator.SetFloat(_velocityXHash, _velocityX);
    }

    private float CalculateCurrentMaxVelocity(bool runPressed)
    {
        bool isCrouched = _animator.GetBool(_isCrouchedHash);
        bool isProne = _animator.GetBool(_isProneHash);
        float currentMaxVelocity;
        if (isProne)
        {
            currentMaxVelocity = maximumProneVelocity;
        }
        else if (isCrouched)
        {
            currentMaxVelocity = maximumCrouchVelocity;
        }
        else
        {
            currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        }

        return currentMaxVelocity;
    }

    private void ChangeVelocity(
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
        if (backwardPressed && _velocityZ > -currentMaxVelocity)
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
        if (backwardPressed && runPressed && IsStanding())
        {
            _velocityZ = 0f;
            _velocityX = 0f;
            _animator.SetBool(_fallDownBackwardsHash, true);
        }
    }

    private void LockOrResetVelocity(
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
        if (backwardPressed && _velocityZ < -currentMaxVelocity)
        {
            _velocityZ = -currentMaxVelocity;
        }
        // round to maximumWalkVelocity if within offset
        else if (
            backwardPressed
            && _velocityZ > -currentMaxVelocity
            && _velocityZ < (-currentMaxVelocity + 0.05f)
        )
        {
            _velocityZ = -currentMaxVelocity;
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

    public float GetVelocityX()
    {
        return _velocityX;
    }

    public float GetVelocityZ()
    {
        return _velocityZ;
    }

    public void Dying()
    {
        _velocityZ = 0f;
        _velocityX = 0f;
        _animator.SetBool(_DyingHash, true);
    }
}
