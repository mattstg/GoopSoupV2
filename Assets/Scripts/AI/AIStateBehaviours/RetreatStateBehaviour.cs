using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatStateBehaviour : GenericStateBehaviour
{
    readonly float fleeDistance = 5;
    //Atm hardcoded, target is always player
    Vector2 posAwayFromPlayer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        posAwayFromPlayer = monster.transform.position - PlayerManager.Instance.player.transform.position;
        posAwayFromPlayer = posAwayFromPlayer.normalized * fleeDistance;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        monster.MoveTowards(posAwayFromPlayer);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

    }
}
