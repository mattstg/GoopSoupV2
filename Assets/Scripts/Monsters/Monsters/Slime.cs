using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster {

    public override void Initialize()
    {
        monsterAggroRange = 4;
        monsterSpeed = 2;
        base.Initialize();
    }

    public override void UpdateMonster(float dt)
    {
        base.UpdateMonster(dt);
    }
}
