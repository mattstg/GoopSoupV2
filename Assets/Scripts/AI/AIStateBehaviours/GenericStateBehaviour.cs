using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStateBehaviour : StateMachineBehaviour
{
    //All the Monster stateBehaviour scripts will inherit from this .Does the caching for Monster automatically, and tells monster when changed states. Easier than copy pasting that into every single state behaviour script

    bool initialized = false;  //if state enter and initialized is false, cache the monster that we are attached to, so we can reference it
    protected Monster monster; //Protected so kids can use it


    private void Initialize(Animator animator)
    {
        monster = animator.gameObject.GetComponent<Monster>(); //the animator component is attached to the same gameobject which has the monster component on it. We cant acces our own gameobject in here, because we are a state behaviour, not a mono behaviour
        initialized = true;

    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        if (!initialized)
            Initialize(animator);
        monster.MonsterEnteredNewState();
   }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
