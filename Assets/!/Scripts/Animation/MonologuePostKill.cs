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
        int currentCounter = animator.GetInteger("idlePostKill") -1;
        animator.SetInteger("idlePostKill",  currentCounter);
        if (currentCounter == 3)
        {
            animator.gameObject.SendMessage("Speak", 3);
        } else
        if (currentCounter == 1)
        {
            animator.gameObject.SendMessage("Speak", 4);
        }  
    }
}
