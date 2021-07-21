using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The monster AI is the brain. It is the one that scans the world looking for player, and setting values in the animator.
 * The animator will then use the statemachine to select the current state, and the state will have a script attached to it
 * example: WanderState.cs
 * In there will be the logic for acting on a state
 * 
 * 
 * There is a bunch of commented code related with a #
 *  that # signifys code that would have been written to create a state machine whose states are not Behaviour scripts. This is if we use GenericBehaviour instead of the ones we are using now
 *  
 */



public class MonsterAI  {


    Monster monster;
    Monster.AIInfo aiInfo;
    Animator anim;
	//BehaviourManager behaviourManager;

    float timeEnteredState;
    float timeInState { get { return Time.time - timeEnteredState; } }



    public MonsterAI(Monster _monster)
    {
        monster = _monster;
        aiInfo = monster.aiInfo;
        anim = monster.gameObject.AddComponent<Animator> ();
		anim.runtimeAnimatorController = GameObject.Instantiate<RuntimeAnimatorController> (Resources.Load ("AI/BaseAI") as RuntimeAnimatorController);
		//behaviourManager = new BehaviourManager (anim, this);
    }


    

    public void StateChanged()
    {
        timeEnteredState = Time.time;
    }


	public void UpdateParameters()
    {
        //The positions of the player and monster turned to shorthand notation
        Vector2 pPos, mPos; 
        pPos = PlayerManager.Instance.player.transform.position;
        mPos = monster.transform.position;

     

        bool seePlayer = false;

        //Determine if we can see the player
        float distance = Vector2.Distance(pPos, mPos);
        if(distance < aiInfo.aggroRange)
        {
            //We are close enough to player, but is there a wall blocking the way? Raycast towards the player and see if we hit ground
            
            seePlayer = true; //We see the player, now see if we set it false
            RaycastHit2D[] rayHits = Physics2D.RaycastAll(mPos, pPos - mPos, distance, LayerMask.GetMask("TerrainElement")); // Testing if we hit any terrain elements
            foreach(RaycastHit2D rh in rayHits)
            {
                seePlayer = false;
            }
        }

        anim.SetBool("SeePlayer", seePlayer);
                
        if(timeInState > aiInfo.attentionSpan)
            anim.SetTrigger("BoredOfState");

        if (monster.bodyInfo.hp <= 0)
            anim.SetTrigger("HasDied");
    }

    public void MonolithDied()
    {
        anim.SetTrigger("MonolithDied");
    }


    public void MonsterHitPlayer()
    {
        anim.SetTrigger("Retreat");
    }



    //When the monster is recycled (depooled) we reset the values
    public void Depooled()
    {
        anim.SetTrigger("ResetStateMachine");
        

        anim.SetBool("SeePlayer", false);
        anim.ResetTrigger("BoredOfState");
        anim.ResetTrigger("HasDied");
        anim.ResetTrigger("HasDied");
        anim.ResetTrigger("MonolithDied");
        anim.ResetTrigger("Retreat");
    }

}
