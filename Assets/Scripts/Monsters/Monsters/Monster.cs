using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class Monster : MonoBehaviour, IPoolable {

	
	MonsterAI ai;
    Rigidbody2D rb;

    public Ingredient ingredientInfo;
    public BodyInfo bodyInfo;
    public AnimInfo animInfo;
    public AIInfo aiInfo;
    public Monolith spawningMonolith;

    public void InitializeMonsterColor(Ingredient ingr)
    {
        ingredientInfo = new Ingredient(ingr);
        GetComponent<SpriteRenderer>().color = ingredientInfo.ToColor();
    }

    public virtual void Initialize(Monolith _spawningMonolith)
    {
        spawningMonolith = _spawningMonolith;
        bodyInfo.hp = bodyInfo.maxHp;
        rb = GetComponent<Rigidbody2D>();
		ai = new MonsterAI (this);          //Atm there is only one kind of AI, so it is just initalized in here
    }

	public virtual void UpdateMonster(float dt){
        ai.UpdateParameters(dt); //This code updates the AI, who in turn calls BehaviourCall
	}

	public virtual void OnTriggerEnter2D(Collider2D coli)
	{

	}

	public void Pooled()
	{
        bodyInfo.hp = bodyInfo.maxHp;
        rb.velocity = new Vector2();
	}

	public void DePooled()
	{
        //Set active again, turn on anim and reset values and etc
        ai.Depooled();
	}

    public void MonolithDied()
    {
        ai.MonolithDied();
    }

	public GameObject GetGameObject { get { return gameObject; } }


    public void MoveTowards(Vector2 targetPos)
    {
        if (Vector2.Distance(transform.position, targetPos) > GV.Monster_Move_TargetReachDist)
        {
            Vector2 goalDir = targetPos - (Vector2)transform.position;
            rb.velocity = goalDir.normalized * bodyInfo.maxSpeed;
        }
        else
        {
            rb.velocity = new Vector2();
        }
    }

    public void ClearVelocity()
    {
        rb.velocity = new Vector2();
    }

    //Custom animation system, since legacy system doesnt handle Sprite changes well
    private void BeginAnimations()
    {

    }

    public void MonsterDies()
    {
        if (spawningMonolith)
            spawningMonolith.MonsterDied(this);
                
        MonsterManager.Instance.RemoveMonster(this);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.TakeDamage(1);
            ai.MonsterHitPlayer();
        }
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
        [Tooltip("Animation time for each keyframe")]
        public float animationSpeed = .2f;
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

    public class Animal
    {
        protected string name, species;
        protected int numOfLegs;

        public Animal(string name, string species, int numOfLegs)
        {
            this.name = name;
            this.species = species;
            this.numOfLegs = numOfLegs;
        }
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name,"Dog",4)
        {
        }
    }


}
