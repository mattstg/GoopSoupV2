using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : BaseState
{

    public AggroState(Monster _monster) : base("Aggro", _monster)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void StayState(float dt)
    {
        monster.MoveTowards(PlayerManager.Instance.player.transform.position);
        base.StayState(dt);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}







