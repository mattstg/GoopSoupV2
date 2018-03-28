using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    MonsterManager monsterManager;
    protected string monsterPrefabName;
    float timeTillNextBreed;
    float timeToBreed;

    public virtual void InitializeMonsterBreeder(MonsterManager _monsterManager, float _timeToBreed)
    {
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
          GameObject  spawnedMonsterObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Monsters/" + monsterPrefabName));
        Monster spawnedMonster = spawnedMonsterObj.GetComponent<Monster>();
        spawnedMonster.Initialize();
        spawnedMonster.transform.position = transform.position + Random.onUnitSphere * 3;
        monsterManager.AddMonster(spawnedMonster);
    }
}
