using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//No idea what this class is, looks like some sort of failed expirement early on or an example to teach something.

public class StateBehaviour_Idle : StateMachineBehaviour
{
    Monster m;
    //public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    Debug.Log("IDle called");
    //    base.OnStateMachineEnter(animator, stateMachinePathHash);
    //   //
    //}
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!m)
            m = animator.gameObject.GetComponent<Monster>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m.MoveTowards(m.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //// OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}
    //
    //// OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
