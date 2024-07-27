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
        int currentCounter = animator.GetInteger("idlePreKill") -1;
        animator.SetInteger("idlePreKill",  currentCounter);
        if (currentCounter == 5)
        {
            animator.gameObject.SendMessage("PlayNextLine");
        } else
        if (currentCounter == 1)
        {
            animator.gameObject.SendMessage("PlayNextLine");
        }        
    }
}
