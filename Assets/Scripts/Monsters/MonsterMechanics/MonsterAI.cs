using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI  {
	static float timeToWander = 2;
    Monster monster;
    Monster.AIInfo aiInfo;
	Animator anim;
	BehaviourManager behaviourManager;

    int currentStateNameHash;       //State machines store thier name as hash values (int representation of the data)
    float timeInCurrentState = 0;


	public MonsterAI(Monster _monster)
    {
        monster = _monster;
        aiInfo = monster.aiInfo;
        anim = monster.gameObject.AddComponent<Animator> ();
		anim.runtimeAnimatorController = GameObject.Instantiate<RuntimeAnimatorController> (Resources.Load ("AI/BaseAI") as RuntimeAnimatorController);
		behaviourManager = new BehaviourManager (anim, monster);
    }

	public void UpdateParameters(float dt)
    {
        //The positions of the player and monster turned to shorthand notation
        Vector2 pPos, mPos; 
        pPos = PlayerManager.Instance.player.transform.position;
        mPos = monster.transform.position;


        //Update time in the current state
        timeInCurrentState += dt;
        int _currentState = anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if(_currentState != currentStateNameHash) //We entered a new state, reset the time in state
        {
            timeInCurrentState = 0;
            currentStateNameHash = _currentState;
        }
        bool seePlayer = false;
        

        //Determine if we can see the player
        float distance = Vector2.Distance(pPos, mPos);
        if(distance < aiInfo.aggroRange)
        {
            seePlayer = true; //We see the player, but is there any obstacles between the two whose height is greater than 0?
            RaycastHit2D[] rayHits = Physics2D.RaycastAll(mPos, pPos - mPos, distance, 1 << LayerMask.NameToLayer("TerrainElement")); // Testing if we hit any terrain elements
            foreach(RaycastHit2D rh in rayHits)
            {
                TerrainElement te = rh.transform.GetComponent<TerrainElement>();
                if (te.height > 0)
                    seePlayer = false;
            }
        }
        anim.SetBool("SeePlayer", seePlayer);

        //Attacks will be done in future, for now monster just rams into you
        //bool inAttackRange = distance < monsterAttachRange;

        //If in idle or wander state too long, it becomes bored and stops chasing
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Wander")) && timeInCurrentState > aiInfo.attentionSpan)
            anim.SetTrigger("BoredOfState");

        //Check if dead
        if (monster.bodyInfo.hp < 0)
            anim.SetTrigger("HasDied");
    }
    
}
