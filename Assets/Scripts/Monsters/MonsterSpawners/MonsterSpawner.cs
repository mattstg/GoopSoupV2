using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    MonsterManager monsterManager;
    GV.MonsterTypes monsterType;
    float timeTillNextBreed;
    float timeToBreed;

    public virtual void InitializeMonsterSpawner(MonsterManager _monsterManager, float _timeToBreed, GV.MonsterTypes _monsterType)
    {
        monsterType = _monsterType;
        monsterManager = _monsterManager;
        timeToBreed = _timeToBreed;
        timeTillNextBreed = Time.time + timeToBreed;
    }

	public virtual void UpdateMonsterSpawner(float dt)
    {
        if (Time.time > timeTillNextBreed)
        {
            SpawnMonster();
            timeTillNextBreed = Time.time + timeToBreed;
        }
    }

    public virtual void SpawnMonster()
    {
        //GameObject spawnedMonsterObj = MasterObjectPool.Instance.GetObjectFromPool(monsterPrefabName);
        //if(!spawnedMonsterObj) //Pool was empty, create a new one
        Monster spawnedMonster = MonsterFactory.Instance.CreateMonster(monsterType.ToString());
        spawnedMonster.Initialize();
        spawnedMonster.transform.position = transform.position + Random.onUnitSphere * 3;
        monsterManager.AddMonster(spawnedMonster);
    }
}
