using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    MonsterManager monsterManager;
    SpawnerInfo spawnerInfo;
    MonsterInfo monsterCreates;
    float timeTillNextBreed;

    public void LinkMonsterManager()
    {
    }

    public virtual void InitializeMonsterSpawner(SpawnerInfo _spawnerInfo, MonsterInfo _monsterCreates, MonsterManager _monsterManager)
    {
        monsterManager = _monsterManager;
        monsterCreates = _monsterCreates;
        spawnerInfo = _spawnerInfo;
        timeTillNextBreed = Time.time + spawnerInfo.spawnRate;
    }

	public virtual void UpdateMonsterSpawner(float dt)
    {
        if (Time.time > timeTillNextBreed)
        {
            SpawnMonster();
            timeTillNextBreed = Time.time + spawnerInfo.spawnRate;
        }
    }

    public virtual void SpawnMonster()
    {
        Monster spawnedMonster = MonsterFactory.Instance.CreateMonster(monsterCreates.monsterType);
        spawnedMonster.Initialize();
        spawnedMonster.transform.position = transform.position + Random.onUnitSphere * 3;
        monsterManager.AddMonster(spawnedMonster);
    }

}
