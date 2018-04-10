using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState  {

    Vector2 wanderTarget;
    readonly float wanderDistance = 6;

    public WanderState(Monster _monster) : base("Wander", _monster)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        wanderTarget = GV.GetRandomSpotNear(monster.transform.position, wanderDistance);
    }

    public override void StayState(float dt)
    {
        base.StayState(dt);
        monster.MoveTowards(wanderTarget);
    }

    public override void ExitState()
    {
        base.ExitState();
        monster.ClearVelocity();
    }
}
