using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolithManager : GenericManager<Monolith> {
    #region
    private static MonolithManager instance;

    private MonolithManager():base() { }

    public static MonolithManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonolithManager();
            }
            return instance;
        }
    }
    #endregion Singleton

    float timeTillnextMonolith;

    public override void Initialize()
    {
        base.Initialize();
        timeTillnextMonolith = Time.time + GV.Monolith_Spawn_Monolith_Rate;
        for(int i = 0; i < GV.Monolith_Spawn_Initial; i++)
            AddItem(Monolith.SpawnRandomMonolith());
    }

    public override void UpdateManager(float dt)
    {
        base.UpdateManager(dt);
        if(timeTillnextMonolith <= Time.time)
        {
            AddItem(Monolith.SpawnRandomMonolith());
            timeTillnextMonolith = Time.time + GV.Monolith_Spawn_Monolith_Rate;
        }
    }
}
