using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("This was used by the old generic statemachine, now is replaced by direct behaviour calls in the state machine (check the states inspector) You'll have to go to the old version to see how this was used")]
public class GenericBehaviour : StateMachineBehaviour {
	/*
	BehaviourManager manager;
    int mahHash;

	public void Initialize(BehaviourManager _manager){
		manager = _manager;
        mahHash = GetHashCode();
	}
		
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		manager.BehaviourOutput (BaseState.StateFunction.Enter, stateInfo.shortNameHash);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		manager.BehaviourOutput (BaseState.StateFunction.Update, stateInfo.shortNameHash);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		manager.BehaviourOutput (BaseState.StateFunction.Exit, stateInfo.shortNameHash);
	}
	*/
}
