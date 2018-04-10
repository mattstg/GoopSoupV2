using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(Monster _monster) : base("AttackTarget", _monster)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void StayState(float dt)
    {
        base.StayState(dt);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
