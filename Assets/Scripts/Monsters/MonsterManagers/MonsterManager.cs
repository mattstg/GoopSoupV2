using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager {

    protected string breederPrefabName;
    MonsterSpawner monsterBreeder;

    public MonsterManager()
    {
        
    }

    public virtual void UpdateMonsterManager(float dt)
    {
        monsterBreeder.UpdateMonsterBreeder(dt);
    }

    public virtual void CreateBreeder(Vector2 loc)
    {
        GameObject mbObj =  GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MonsterBreeders/" + breederPrefabName));
        monsterBreeder = mbObj.GetComponent<MonsterSpawner>();
        monsterBreeder.InitializeMonsterBreeder(GV.Monster_Breed_Time);
    }
}
