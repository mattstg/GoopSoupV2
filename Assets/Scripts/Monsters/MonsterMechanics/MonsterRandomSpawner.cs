using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRandomSpawner  {

    #region Singleton
    private static MonsterRandomSpawner instance;

    private MonsterRandomSpawner() { }

    public static MonsterRandomSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterRandomSpawner();
            }
            return instance;
        }
    }
    #endregion


    float timeOfSummoning = 0;
    float timeBetweenSummons = 10;


    //Randomly summons a monster on the map, temporary measure until Monoliths are added
    public void Update(float dt)
    {
        if(Time.time >= timeOfSummoning)
        {
            MonsterFactory.Instance.CreateMonster();            
            timeOfSummoning = Time.time + timeBetweenSummons;
        }
    }

}
