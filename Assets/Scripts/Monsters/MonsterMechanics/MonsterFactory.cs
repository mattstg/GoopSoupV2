using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory {
    #region Singleton
    private static MonsterFactory instance;

    public static MonsterFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterFactory();
            }
            return instance;
        }
    }
    #endregion

    //Used for storing System.Types, can be used to AddComponents to objects
    Dictionary<GV.MonsterTypes, MonsterInfoPair> typeOfMonsterDict;

    private MonsterFactory()
    {
        typeOfMonsterDict = new Dictionary<GV.MonsterTypes, MonsterInfoPair>()
        {
            {GV.MonsterTypes.Slime, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Slime, typeof(Slime)   ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Slime, 10, 5,    0f,2))},
            {GV.MonsterTypes.Wisp,  new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Wisp,  typeof(Monster) ,1, .5f,  1), new SpawnerInfo(GV.MonsterTypes.Wisp,  10, 3.5f, 0f,2))},
            {GV.MonsterTypes.Golem, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Golem, typeof(Monster) ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Golem, 10, 15,   0f,2))},
            {GV.MonsterTypes.Chicken, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Chicken, typeof(Monster) ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Chicken, 10, 3,   0f,2))}
        };
    }

    private class MonsterInfoPair
    {
        public MonsterInfo monsterInfo;
        public SpawnerInfo spawnerInfo;

        public MonsterInfoPair(MonsterInfo _minfo, SpawnerInfo _sinfo)
        {
            monsterInfo = _minfo;
            spawnerInfo = _sinfo;
        }
    }


    public Monster CreateMonster(GV.MonsterTypes monsterType)
    {
        GameObject toRetObj = null;
        IPoolable poolable = ObjectPool.Instance.RetrieveFromPool(monsterType.ToString());
        if (poolable != null)
            toRetObj = poolable.GetGameObject;
        else
            toRetObj = CreateMonsterComplex(monsterType);
        Monster toRet = toRetObj.GetComponent<Monster>();
        if (!toRet)
            Debug.LogError("Something went wrong in monster factory, object: " + toRetObj.name + " did not contain a monster script. Returning Null");
        return toRet;
    }

    public MonsterSpawner CreateMonsterSpawner(GV.MonsterTypes monsterType, MonsterManager monsterManager)
    {
        string spawnerName = monsterType.ToString() + "Spawner";
        if (!typeOfMonsterDict.ContainsKey(monsterType))
        {
            Debug.LogError("Create Spawner could not find spawner: " + spawnerName + " in the typeOfMonsterDict");
            return null;
        }
        SpawnerInfo spawnerInfo = typeOfMonsterDict[monsterType].spawnerInfo;

        GameObject spawnerObj = new GameObject();
        spawnerObj.name = spawnerName;
        spawnerObj.tag = "Spawner";
        spawnerObj.layer = LayerMask.NameToLayer("Spawner");

        SpriteRenderer sr = spawnerObj.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/MonsterSpawners/" + spawnerName);
        sr.sortingLayerName = "Spawner";

        //Only if moving 
        if(spawnerInfo.movespeed != 0)
        {
            Rigidbody2D rb = spawnerObj.AddComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 0;
        }

        
        spawnerObj.AddComponent<BoxCollider2D>();
        MonsterSpawner mspawner = spawnerObj.AddComponent<MonsterSpawner>();
        mspawner.InitializeMonsterSpawner(spawnerInfo, typeOfMonsterDict[monsterType].monsterInfo, monsterManager);
        //Animation system here if exists
        return mspawner;
    }


    /*private GameObject CreateMonsterSimple(string monsterName)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Monsters/" + monsterName));
    }*/

    private GameObject CreateMonsterComplex(GV.MonsterTypes monsterType)
    {
        string monsterName = monsterType.ToString();
        if(!typeOfMonsterDict.ContainsKey(monsterType))
        {
            Debug.LogError("CreateMonsterComplex could not find monster: " + monsterName + " in the typeOfMonsterDict");
            return null;
        }
        GameObject newMonster = new GameObject();
        newMonster.name = monsterName;
        newMonster.tag = "Monster";
        newMonster.layer = LayerMask.NameToLayer("Monster");

        MonsterInfo monsterInfo = typeOfMonsterDict[monsterType].monsterInfo;
        newMonster.AddComponent(monsterInfo.monsterScriptType);
        SpriteRenderer sr = newMonster.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Monsters/" + monsterName);
        sr.sortingLayerName = "Monster";
        Rigidbody2D rb = newMonster.AddComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;
        newMonster.AddComponent<BoxCollider2D>();
        //Animations are a bit more complex, we'll save that for later -- AnimatorOverrideController
        //AnimationClip auto generate        
        return newMonster;
    }

    private GameObject CreateMonsterAdvanced(string monsterName)
    {
        //This will use the auto class generator which will be covered at a later date (Course 3)
        return null;
    }

}
