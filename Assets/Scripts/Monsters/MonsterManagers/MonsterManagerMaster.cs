﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManagerMaster {

    #region Singleton
    private static MonsterManagerMaster instance;

    private MonsterManagerMaster() { }

    public static MonsterManagerMaster Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterManagerMaster();
            }
            return instance;
        }
    }
    #endregion

    public Dictionary<GV.MonsterTypes, MonsterManager> monsterManagers = new Dictionary<GV.MonsterTypes, MonsterManager>();
    //public float testVar { private set; get; }

    public void InitializeInitialMonsterManagers()
    {
        SpawnRandomMonsterBreeder();
        //testVar = 5;
        //Debug.Log("testvar: " + testVar);
    }

    

    /// <summary>
    /// This function creates a monster breeder and a manager to match with it
    /// </summary>
    public void SpawnRandomMonsterBreeder()
    {
        //At the moment, Random is not implemented for one type, will show generics later for that
        Vector2 loc = new Vector2(Random.Range(0, GV.Map_Size_XY), Random.Range(0, GV.Map_Size_XY));
        GV.MonsterTypes randMonsterType = GV.GetRandomEnum<GV.MonsterTypes>();
        if (!monsterManagers.ContainsKey(randMonsterType))
        {
            MonsterManager mm = new MonsterManager(randMonsterType);
            mm.CreateBreeder(loc);
            monsterManagers.Add(randMonsterType, mm);
        }
    }

    public void UpdateMonsterManagers(float dt)
    {
        foreach (KeyValuePair<GV.MonsterTypes, MonsterManager> kv in monsterManagers)
            kv.Value.UpdateMonsterManager(dt);
    }

}
