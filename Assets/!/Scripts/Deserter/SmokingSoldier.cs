using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingSoldier : MonoBehaviour
{
    public GameObject lonelyDeserter;
    private Animator _animator;
    private Deserter _deserterScript;
    
    private int _doExecuteHash;

    // Start is called before the first frame update
    void Start()
    {
        _deserterScript = lonelyDeserter.GetComponent<Deserter>();
        _animator = GetComponent<Animator>();
        _doExecuteHash = Animator.StringToHash("DoExecute");
    }

    public void SkipExecutionAnimation()
    {
        _animator.SetBool(_doExecuteHash, false);
        _deserterScript.SkipDeath();
    }

    private void KillDeserter()
    {
        AudioManager.Instance.PlaySoundFx("k98");
        _deserterScript.GettingKilled();
    }
}
