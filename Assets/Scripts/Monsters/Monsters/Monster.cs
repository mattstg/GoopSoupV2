using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolable {
	public enum State { Enter, Update, Exit }
    enum MonsterMode {Idle, Attacking, Moving,  }
    MonsterMode monsterMode = MonsterMode.Idle;
	MonsterAI ai;
    Rigidbody2D rb;
    Vector2 target;
	float hp = 100;

	bool seePlayer, attackRange;

    protected float monsterAggroRange;
	protected float monsterAttachRange;
    protected float monsterSpeed;

    public virtual void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
		ai = new MonsterAI (this);
    }

	public virtual void UpdateMonster(float dt){
		float distance = Vector2.Distance (PlayerManager.Instance.player.transform.position, transform.position);
		seePlayer = distance < monsterAggroRange;
		attackRange = distance < monsterAttachRange;
		ai.Refresh (dt, seePlayer, attackRange);

		if (hp <= 0)
			ai.Dies ();
	}

	public virtual void BehaviourCall(string name, State state)
    {
		switch (name)
        {
		case "Idle":
			Idle (state);
			break;
		case "Die":
			Die (state);
			break;
		case "AttackTarget":
			AttackTarget (state);
			break;
		case "ChaseTarget":
			ChaseTarget (state);
			break;
		case "Wander":
			Wander (state);
			break;
		default:
			Debug.Log ("Your Behaviour dosn't match any known Behaviour.");
			break;
        }
    }

	protected virtual void Idle(State state)
	{
		switch (state) {
		case State.Enter:
			//enter stuff
			break;
		case State.Update:
			//update stuff
			break;
		case State.Exit:
			//exit stuff
			break;
		default:
			Debug.Log ("You seem to have taken a wrong turn.");
			break;
		}
	}

	protected virtual void Die(State state)
	{
		switch (state) {
		case State.Enter:
			//enter stuff
			break;
		case State.Update:
			//update stuff
			break;
		case State.Exit:
			//exit stuff
			break;
		default:
			Debug.Log ("You seem to have taken a wrong turn.");
			break;
		}
	}

	protected virtual void AttackTarget(State state)
	{
		switch (state) {
		case State.Enter:
			//enter stuff
			break;
		case State.Update:
			//update stuff
			break;
		case State.Exit:
			//exit stuff
			break;
		default:
			Debug.Log ("You seem to have taken a wrong turn.");
			break;
		}
	}

	protected virtual void ChaseTarget(State state)
	{
		switch (state) {
		case State.Enter:
			//enter stuff
			break;
		case State.Update:
			//update stuff
			break;
		case State.Exit:
			//exit stuff
			break;
		default:
			Debug.Log ("You seem to have taken a wrong turn.");
			break;
		}
	}

	protected virtual void Wander(State state)
	{
		switch (state) {
		case State.Enter:
			//enter stuff
			break;
		case State.Update:
			//update stuff
			break;
		case State.Exit:
			//exit stuff
			break;
		default:
			Debug.Log ("You seem to have taken a wrong turn.");
			break;
		}
	}

	public virtual void OnCollisionEnter2D(Collision2D coli)
	{

	}

	public virtual void OnTriggerEnter2D(Collider2D coli)
	{

	}

	public void Pooled()
	{
		//Turn off things that matter, like animation
	}

	public void DePooled()
	{
		//Set active again, turn on anim and reset values and etc
	}

	public GameObject GetGameObject { get { return gameObject; } }

	/*
    protected virtual void MoveUpdate(Vector2 targetPos)
    {
        if (Vector2.Distance(transform.position, targetPos) < .2f)
        {
            monsterMode = MonsterMode.Idle;
            rb.velocity = new Vector2();
        }
        else
        {
            Vector2 goalDir = targetPos - (Vector2)transform.position;
            rb.velocity = goalDir.normalized * monsterSpeed;
        }
        
    }
    */
}
