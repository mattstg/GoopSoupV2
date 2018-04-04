using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolable {

    enum MonsterMode {Idle, Attacking, Moving,  }
    MonsterMode monsterMode = MonsterMode.Idle;
    Rigidbody2D rb;
    Vector2 target;

    protected float monsterAggroRange;
    protected float monsterSpeed;

    public virtual void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	public virtual void UpdateMonster(float dt)
    {
        switch (monsterMode)
        {
            case MonsterMode.Idle:
                IdleUpdate();
                break;
            case MonsterMode.Attacking:
                break;
            case MonsterMode.Moving:
                MoveUpdate(target);
                break;
            default:
                break;
        }
    }

    protected virtual void AttackUpdate()
    {

    }

    protected virtual void IdleUpdate()
    {
        if(Vector2.Distance(PlayerManager.Instance.player.transform.position, transform.position) < monsterAggroRange)
        {
            target = PlayerManager.Instance.player.transform.position;
            monsterMode = MonsterMode.Moving;
        }

    }

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

    protected virtual void Idle()
    {

    }

    public virtual void OnCollisionEnter(Collision2D coli)
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
}
