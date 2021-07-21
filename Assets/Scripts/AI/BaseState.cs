using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Obsolete("This was used by the old generic statemachine, now is replaced by direct behaviour calls in the state machine (check the states inspector) You'll have to go to the old version to see how this was used")]
public class BaseState  {

    public enum StateFunction { Enter, Update, Exit }

    public string name;
    protected Monster monster;

    public BaseState(string _name, Monster _monster)
    {
        name = _name;
        monster = _monster;
    }

    public virtual void EnterState()
    {
    }

    public virtual void StayState(float dt)
    {
    }

    public virtual void ExitState()
    {
    }
}
