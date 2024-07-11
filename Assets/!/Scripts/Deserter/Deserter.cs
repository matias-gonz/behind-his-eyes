using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserter : MonoBehaviour
{
    private Animator _animator;
    private int _deathHash;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _deathHash = Animator.StringToHash("Death");
    }

    void Update() { }

    public void GettingKilled()
    {
        _animator.SetBool(_deathHash, true);
    }
}
