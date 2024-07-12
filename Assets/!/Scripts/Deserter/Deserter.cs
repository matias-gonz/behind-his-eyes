using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserter : MonoBehaviour
{
    private Animator _animator;
    private int _deathHash;
    private int _skipDeathHash  ;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _deathHash = Animator.StringToHash("Death");
        _skipDeathHash = Animator.StringToHash("skipDeath");
    }

    void Update() { }

    public void GettingKilled()
    {
        _animator.SetBool(_deathHash, true);
    }

    public void SkipDeath()
    {
        _animator.SetBool(_skipDeathHash , true);
    }
}
