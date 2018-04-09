using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI  {
	static float timeToWander = 2;
    Monster monster;
	Animator anim;
	BehaviourManager behaviourManager;
	float idleTimer = 0;
	bool wandering = false;
	bool agressive = false;

	public MonsterAI(Monster _monster)
    {
        monster = _monster;
		anim = monster.GetComponent<Animator> ();
		anim.runtimeAnimatorController = RuntimeAnimatorController.Instantiate<RuntimeAnimatorController> (Resources.Load ("Resources/AI/BaseAI") as RuntimeAnimatorController);
		behaviourManager = new BehaviourManager (anim, monster);
    }

	public void Refresh(float dt, bool seePlayer, bool attackRange){
		anim.SetBool ("SeePlayer", seePlayer);
		anim.SetBool ("SeePlayer", attackRange);

		if (!agressive && seePlayer) {
			agressive = true;
			idleTimer = 0;
			wandering = false;
		} else if (agressive && !seePlayer)
			agressive = false;

		if (!agressive && !wandering) {
			idleTimer += dt;
			if (idleTimer >= timeToWander) {
				anim.SetTrigger ("Wander");
				wandering = true;
			}
		}
	}

	public void Dies(){
		anim.SetTrigger ("Dies");
	}

}
