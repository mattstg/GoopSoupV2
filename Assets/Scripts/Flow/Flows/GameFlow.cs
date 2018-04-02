using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {
    //https://codeblog.jonskeet.uk/2008/08/09/making-reflection-fly-and-exploring-delegates/
    ParticleManager pm;
    float timeOfSummonNextMonsterSpawn = 0;

	public override void InitializeFlow()
    {
        pm = new ParticleManager();
        pm.Setup();
        ParticleSystem ps = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("PSys")).GetComponent<ParticleSystem>();
        pm.SetSystemValues(ps, ParticleManager.ModuleType.Main, "duration", 15);
        float f = ps.main.duration;
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
