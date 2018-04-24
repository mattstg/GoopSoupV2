using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    readonly float fleeDistance = 5;
    //Atm hardcoded, target is always player
    Vector2 posAwayFromPlayer;

    public RetreatState(Monster _monster) : base("Retreat", _monster)
    {

    }

    public override void EnterState()
    {
        posAwayFromPlayer = monster.transform.position - PlayerManager.Instance.player.transform.position;
        posAwayFromPlayer = posAwayFromPlayer.normalized * fleeDistance;
        base.EnterState();
    }

    public override void StayState(float dt)
    {
        monster.MoveTowards(posAwayFromPlayer);
        base.StayState(dt);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
