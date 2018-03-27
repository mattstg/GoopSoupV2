using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    protected string monsterPrefabName;
    float timeTillNextBreed;
    float timeToBreed;
    List<Monster> childMonsters;

    public virtual void InitializeMonsterBreeder(float _timeToBreed)
    {
        childMonsters = new List<Monster>();
        timeToBreed = _timeToBreed;
        timeTillNextBreed = Time.time + timeToBreed;
    }

	public virtual void UpdateMonsterBreeder(float dt)
    {
        if (Time.time > timeTillNextBreed)
        {
            SpawnMonster();
            timeTillNextBreed = Time.time + timeToBreed;
        }
        foreach (Monster m in childMonsters)
            m.UpdateMonster(dt);
    }

    public virtual void SpawnMonster()
    {
        GameObject spawnedMonster = MasterObjectPool.Instance.GetObjectFromPool(monsterPrefabName);
        if(!spawnedMonster) //Pool was empty, create a new one
            spawnedMonster = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Monsters/" + monsterPrefabName));
        childMonsters.Add(spawnedMonster.GetComponent<Monster>());
        spawnedMonster.GetComponent<Monster>().Initialize();
        spawnedMonster.transform.position = transform.position + Random.onUnitSphere * 3;
    }
}
