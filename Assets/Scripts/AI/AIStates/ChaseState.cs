using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    //Atm hardcoded, target is always player

    public ChaseState(Monster _monster) : base("ChaseTarget", _monster)
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
