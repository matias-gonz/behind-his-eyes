using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyCounterReset : StateMachineBehaviour
{
    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        animator.SetBool("Action1", false);
        animator.SetBool("Action2", false);
        animator.SetBool("Action3", false);
    }
}
