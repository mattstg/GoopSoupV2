using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameFlow : Flow {

	public override void InitializeFlow()
    {
        PlayerManager.Instance.Initialize();
        MonsterManagerMaster.Instance.InitializeInitialMonsterManagers();
    }

    public override void UpdateFlow(float dt)
    {
        PlayerManager.Instance.Update(dt);
        MonsterManagerMaster.Instance.UpdateMonsterManagers(dt);
        MonsterRandomSpawner.Instance.Update(dt);
    }

    public override void FixedUpdateFlow(float dt)
    {
        PlayerManager.Instance.FixedUpdate(dt);
    }
}
