using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class Monster : MonoBehaviour, IPoolable {

    public string spriteName;
	public enum State { Enter, Update, Exit }
    enum MonsterMode {Idle, Attacking, Moving}
    MonsterMode monsterMode = MonsterMode.Idle;
	MonsterAI ai;
    Rigidbody2D rb;
    Vector2 target;

    [Tooltip("Ingredient value of the creature")]
    public ColorInit ingredientValue;
    public BodyInfo bodyInfo;
    public AnimInfo animInfo;
    public AIInfo aiInfo;

    public virtual void Initialize()
    {
        bodyInfo.hp = bodyInfo.maxHp;
        rb = GetComponent<Rigidbody2D>();
		ai = new MonsterAI (this);          //Atm there is only one kind of AI, so it is just initalized in here
    }

	public virtual void UpdateMonster(float dt){
        ai.UpdateParameters(dt); //This code updates the AI, who in turn calls BehaviourCall
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
            rb.velocity = goalDir.normalized * bodyInfo.maxSpeed;
        }        
    }

    //Custom animation system, since legacy system doesnt handle Sprite changes well
    private void BeginAnimations()
    {

    }


   
    [System.Serializable]
    public class BodyInfo
    {
        public float maxHp, accel, maxSpeed;
        [HideInInspector] public float hp;

    }

    [System.Serializable]
    public class AnimInfo
    {
        public string spriteName;
        [Tooltip("Time to preform one anim full loop")]
        public float animationSpeed = 1;
    }

    [System.Serializable]
    public class AIInfo
    {
        public float aggroRange, attentionSpan;
    }

    //This class is just that R-G-B appears in the unity editor, instead of a color selection panel
    [System.Serializable]
    public class ColorInit
    {
        public float r, g, b;
    }
}
