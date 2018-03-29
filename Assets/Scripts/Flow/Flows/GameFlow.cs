﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {

    float timeOfSummonNextMonsterSpawn = 0;

	public override void InitializeFlow()
    {
        PlayerManager.Instance.Initialize();
        MonsterManagerMaster.Instance.InitializeInitialMonsterManagers();
        timeOfSummonNextMonsterSpawn = Time.time + GV.Spawner_Summon_Time;
    }

    public override void UpdateFlow(float dt)
    {
        PlayerManager.Instance.Update(dt);
        MonsterManagerMaster.Instance.UpdateMonsterManagers(dt);
        if(Time.time >= timeOfSummonNextMonsterSpawn)
        {
            MonsterManagerMaster.Instance.SpawnRandomMonsterBreeder();
            timeOfSummonNextMonsterSpawn = Time.time + GV.Spawner_Summon_Time;
        }
    }

    public override void FixedUpdateFlow(float dt)
    {
        PlayerManager.Instance.FixedUpdate(dt);
    }
}
