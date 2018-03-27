using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager {

    protected string breederPrefabName;
    MonsterSpawner monsterBreeder;
    List<Monster> monsterList;
    GV.MonsterTypes monsterType;

    public MonsterManager(GV.MonsterTypes _monsterType)
    { //Each child monster Manager sets the breederPrefabName, in the future we want to use enums to make it more efficient
        monsterList = new List<Monster>();
        monsterType = _monsterType;
    }

    public void UpdateMonsterManager(float dt)
    {
        monsterBreeder.UpdateMonsterSpawner(dt);
        foreach (Monster m in monsterList)
            m.UpdateMonster(dt);
    }

    public void CreateBreeder(Vector2 loc)
    {
        GameObject mbObj =  GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MonsterSpawners/" + monsterType + "Spawner"));
        monsterBreeder = mbObj.GetComponent<MonsterSpawner>();
        monsterBreeder.InitializeMonsterBreeder(this, GV.Monster_Breed_Time);
    }

    public void AddMonster(Monster toAdd)
    {
        if (!monsterList.Contains(toAdd)) //should never happen, but just in case!
            monsterList.Add(toAdd);
    }

    public void RemoveMonster(Monster toRemove)
    {
        monsterList.Remove(toRemove); //Lists are remove safe, if the element isn't in it it does nothing
    }
}
