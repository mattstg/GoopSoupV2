using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameFlow : Flow {

	public override void InitializeFlow()
    {
        LevelGenerator.Instance.GenerateWorldMap(GV.Map_Size_XY);
        PlantsManager.Instance.Init();
        PlayerManager.Instance.Initialize();
        // MonsterFactory.Instance.InitializeFactory();
    }

    public override void UpdateFlow(float dt)
    {
        PlantsManager.Instance.Update(dt);
        PlayerManager.Instance.Update(dt);
        // MonsterRandomSpawner.Instance.Update(dt);
        // MonsterManager.Instance.UpdateMonsterManager(dt);
    }

    public override void FixedUpdateFlow(float dt)
    {
        PlayerManager.Instance.FixedUpdate(dt);
    }
}
