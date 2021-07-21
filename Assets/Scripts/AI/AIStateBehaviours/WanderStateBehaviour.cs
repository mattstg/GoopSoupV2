using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderStateBehaviour : GenericStateBehaviour
{
    Vector2 wanderTarget;
    readonly float wanderDistance = 6;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        wanderTarget = GV.GetRandomSpotNear(monster.transform.position, wanderDistance);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        monster.MoveTowards(wanderTarget);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        monster.ClearVelocity();
    }
}
