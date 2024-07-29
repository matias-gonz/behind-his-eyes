using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGuardAnimationController : MonoBehaviour
{
    private Animator _animator;
    public bool isSmoking = false;
    private int _isSmokingHash;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _isSmokingHash = Animator.StringToHash("IsSmoking");
        _animator.SetBool(_isSmokingHash, isSmoking);
    }
}
