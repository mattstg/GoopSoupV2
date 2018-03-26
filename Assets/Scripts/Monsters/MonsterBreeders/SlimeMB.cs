using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMB : MonsterBreeder {

    public override void InitializeMonsterBreeder(float _timeToBreed)
    {
        base.InitializeMonsterBreeder(_timeToBreed);
        monsterPrefabName = "Slime";
    }

    public override void UpdateMonsterBreeder(float dt)
    {
        base.UpdateMonsterBreeder(dt);
    }

}
