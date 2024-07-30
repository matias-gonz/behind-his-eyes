using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyCounter : StateMachineBehaviour
{

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        int currentCounter = animator.GetInteger("IdleCounter") -1;
        animator.SetInteger("IdleCounter",  currentCounter);
        if (currentCounter == 0)
        {         
            animator.SetInteger("IdleCounter",  4);
        }
    }
    
}
