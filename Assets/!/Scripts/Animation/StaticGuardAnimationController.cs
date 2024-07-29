using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticGuardAnimationController : MonoBehaviour
{
    private Animator _animator;
    public bool isSmoking = false;
    public bool alternativeAnimation = false;
    private int _isSmokingHash;
    private int _alternativeAnimationHash;
    private int _idleCounterHash;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _isSmokingHash = Animator.StringToHash("IsSmoking");
        _alternativeAnimationHash = Animator.StringToHash("AlternativeAnimation");
        _idleCounterHash = Animator.StringToHash("IdleCounter");

        _animator.SetBool(_isSmokingHash, isSmoking);
        _animator.SetBool(_alternativeAnimationHash, alternativeAnimation);
        _animator.SetInteger(_idleCounterHash, Random.Range(1,6));
        
    }
}
