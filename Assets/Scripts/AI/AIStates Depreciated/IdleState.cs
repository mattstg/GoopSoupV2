using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Monster _monster) : base("Idle",_monster)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        monster.ClearVelocity();
    }

    public override void StayState(float dt)
    {
        base.StayState(dt);
        monster.ClearVelocity();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
