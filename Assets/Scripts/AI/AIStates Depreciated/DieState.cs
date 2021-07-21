using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    public DieState(Monster _monster) : base("Die",_monster)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        monster.MonsterDies();
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
