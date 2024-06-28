using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : StateMachineBehaviour
{
    // public CharacterController controller;
    public ThirdPersonMovement myThirdPersonMovement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("fallDownBackwards", false);
    }
}
