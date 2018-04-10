using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
