using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologuePreKill : StateMachineBehaviour
{
    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        animator.SetInteger("idlePreKill", animator.GetInteger("idlePreKill") - 1);
    }
}
    