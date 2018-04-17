using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameFlow : Flow {

	public override void InitializeFlow()
    {
        LevelGenerator.Instance.GenerateWorldMap(GV.Map_Size_XY);
        PlayerManager.Instance.Initialize();
        MonsterFactory.Instance.InitializeFactory();
        PlantManager.Instance.Initialize();
        PlantManager.Instance.FillWorldWithPlants(GV.Plants_InitialSpawnCount);
        MonolithManager.Instance.Initialize();
    }

    public override void UpdateFlow(float dt)
    {
        PlayerManager.Instance.Update(dt);
        //MonsterRandomSpawner.Instance.Update(dt);
        MonsterManager.Instance.UpdateMonsterManager(dt);
        PlantManager.Instance.UpdatePlants(dt);
        MonolithManager.Instance.UpdateManager(dt);
    }

    public override void FixedUpdateFlow(float dt)
    {
        PlayerManager.Instance.FixedUpdate(dt);
    }
}
