using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFiring : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("FireSingleShot", false);
    }
}
