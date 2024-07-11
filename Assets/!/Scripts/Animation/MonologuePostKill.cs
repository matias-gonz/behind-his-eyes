using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologuePostKill : StateMachineBehaviour
{
    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        animator.SetInteger("idlePostKill", animator.GetInteger("idlePostKill") - 1);
    }
}
