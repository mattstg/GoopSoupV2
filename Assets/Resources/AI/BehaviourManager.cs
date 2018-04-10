using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager  {
	//must be all the animation names which have behaviour scripts
	
	private Dictionary<int,string> hashToName = new Dictionary<int, string>();
	StateMachineBehaviour[] behaviour;
	Animator anim;
	MonsterAI monsterAI;

	public BehaviourManager(Animator _anim, MonsterAI _monsterAI){
		anim = _anim;
        monsterAI = _monsterAI;
		behaviour = anim.GetBehaviours<GenericBehaviour> ();
		foreach(GenericBehaviour gb in behaviour)
			gb.Initialize (this);

        foreach (string s in MonsterAI.stateNames)
            hashToName.Add(Animator.StringToHash(s), s);
	}

    //When the game object is turned off, generic behaviours some reason lose thier links? Maybe a weird mono : StateMachineBehaviour thing?
    //So when brought off the pool, need to link again
    public void Depooled()
    {
        //There be me a memory leak here!!! GenericBehaviours seem to change when re-enabled, but the old ones still exist?
        behaviour = anim.GetBehaviours<GenericBehaviour>();
        foreach (GenericBehaviour gb in behaviour)
            gb.Initialize(this);
    }

	private string GetName(int nameHash){
		return hashToName [nameHash];
	}

	public void BehaviourOutput(BaseState.StateFunction state, int hash){
		string name = GetName(hash);
        monsterAI.BehaviourCall(name, state);


        //Debug.Log ("State Name: " + name + ". Current phase: " + state.ToString());
	}
}
