using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager  {
	//must be all the animation names which have behaviour scripts
	static string[] stateName = {"Idle","Die","AttackTarget","ChaseTarget","Wander"};
	private Dictionary<int,string> hashToName = new Dictionary<int, string>();
	StateMachineBehaviour[] behaviour;
	Animator anim;
	Monster monster;

	public BehaviourManager(Animator _anim, Monster _monster){
		anim = _anim;
		monster = _monster;
		behaviour = anim.GetBehaviours<GenericBehaviour> ();
		foreach(GenericBehaviour gb in behaviour)
			gb.Initialize (this);

		foreach (string s in stateName)
			hashToName.Add (Animator.StringToHash (s), s);
	}

	private string GetName(int nameHash){
		return hashToName [nameHash];
	}

	public void BehaviourOutput(Monster.State state, int hash){
		string name = GetName(hash);

		Debug.Log ("State Name: " + name + ". Current phase: " + state.ToString());
	}
}
