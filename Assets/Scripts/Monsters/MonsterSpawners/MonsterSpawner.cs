using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    MonsterManager monsterManager;
    SpawnerInfo spawnerInfo;
    MonsterInfo monsterCreates;
    float timeTillNextBreed;
    static readonly float MONOLITH_SPAWN_DISTANCE_MAX = 3;

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
        Vector2 loc = transform.position + Random.onUnitSphere * MONOLITH_SPAWN_DISTANCE_MAX;
        Monster spawnedMonster = MonsterFactory.Instance.CreateMonster(monsterCreates.monsterType.ToString(), loc);
        monsterManager.AddMonster(spawnedMonster);
    }

}
