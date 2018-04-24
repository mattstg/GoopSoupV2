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

    Dictionary<string, GameObject> monsterPrefabDict;

    string monsterPrefabPath = "Prefabs/Monsters/";
    string[] monsterNames;
    int monsterLayer;

    

    private MonsterFactory()
    {
        monsterLayer = LayerMask.NameToLayer("Monster");
    }

    public string GetRandomMonsterName()
    {
        return GV.GetRandomElemFromArr<string>(monsterNames);
    }

    public void InitializeFactory()
    {
        //Loads all prefabs into the factory
        monsterPrefabDict = new Dictionary<string, GameObject>();
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>(monsterPrefabPath);
        foreach (GameObject prefab in allPrefabs)
        {
            //Safety checks, did they create the monster properly?  
            Monster newMonster = prefab.GetComponent<Monster>();
            if (newMonster)
            {
                if (!newMonster.CompareTag("Monster"))
                    Debug.Log("Monster: " + prefab.name + " used the tag: " + newMonster.tag + " instead of Monster, was that on purpose?");
                if (newMonster.gameObject.layer != monsterLayer)
                    Debug.Log("Monster: " + prefab.name + " used the layer: " + LayerMask.LayerToName(newMonster.gameObject.layer) + " instead of Monster, was that on purpose?");
                if (!newMonster.GetComponent<Collider2D>())
                    Debug.Log("Monster: " + prefab.name + " does not have a collider was that on purpose?");
                if (newMonster.bodyInfo.maxHp <= 0)
                    Debug.LogError("Monster: " + prefab.name + " maxHp is <= 0!! dies on spawn!");

                newMonster.GetComponent<SpriteRenderer>().sortingLayerName = "Monster";
                //Setup the Rigidbody - If a developer wants a different setup, this would not allow it, so must be careful and mention this in meetings
                Rigidbody2D rb = newMonster.GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.gravityScale = 0;

                monsterPrefabDict.Add(prefab.name, prefab);
            }
            else
            {
                Debug.Log("A prefab inside of Assets/Resources/" + monsterPrefabPath + " did not have a monster script, only have monster prefabs in this folder! prefab name: " + prefab.name);
            }
        }

        List<string> tempList = new List<string>(monsterPrefabDict.Keys);
        monsterNames = tempList.ToArray();
    }


    //External calls used by the other developers, I include may overloads for ease of use.

    /// <summary>
    /// Creates a random monster at random location
    /// </summary>
    public Monster CreateMonster()
    {
        string monsterName = GV.GetRandomElemFromArr<string>(monsterNames);
        Vector2 location = GV.GetRandomSpotInMap();
        return CreateMonster(monsterName, location, Ingredient.RandomIngredient());
    }

    /// <summary>
    /// Creates named monster at random location
    /// </summary>
    public Monster CreateMonster(string monsterName, Ingredient ingr)
    {
        Vector2 location = GV.GetRandomSpotInMap();
        return CreateMonster(monsterName, location, ingr);
    }

    /// <summary>
    /// Creates random monster at given location
    /// </summary>
    public Monster CreateMonster(Vector2 loc)
    {
        string monsterName = GV.GetRandomElemFromArr<string>(monsterNames);
        return CreateMonster(monsterName, loc, Ingredient.RandomIngredient());
    }


    //Main function used by everyone and by the functions above
    /// <summary>
    /// Creates a monster of given name at the location
    /// </summary>
    public Monster CreateMonster(string monsterName, Vector2 loc, Ingredient monsterIngredient)
    {
        GameObject toRetObj = null;
        IPoolable poolable = ObjectPool.Instance.RetrieveFromPool(monsterName); //atm pool is not functional, will always return null
        if (poolable != null)
            toRetObj = poolable.GetGameObject;
        else
            toRetObj = _CreateMonster(monsterName).gameObject;
        Monster toRet = toRetObj.GetComponent<Monster>();
        if (!toRet)
            Debug.LogError("Something went wrong in monster factory, object: " + toRetObj.name + " did not contain a monster script. Returning Null");
        else
        {
            toRet.transform.position = loc;
            MonsterManager.Instance.AddMonster(toRet);
        }
        toRet.InitializeMonsterColor(monsterIngredient);
        return toRet;
    }


    //Internal call, used by the factory
    private Monster _CreateMonster(string monsterName)
    {
        if(!monsterPrefabDict.ContainsKey(monsterName))
        {
            Debug.LogError("Monster of name: " + monsterName + " not found in monsterDict");
            return null;
        }

        GameObject newMonsterObj = GameObject.Instantiate(monsterPrefabDict[monsterName]);
        newMonsterObj.name = monsterName; //To remove the (Clone) part
        Monster newMonster = newMonsterObj.GetComponent<Monster>();
        AnimationFactory.Instance.SetupAnimationForMonster(newMonster);


        //Setup the AI
        //Atm there is only one type of AI (State machine), so we will initialize that inside of Monster

        newMonster.Initialize();
        if (GV.DEBUG_Monsters_Triggers)
            newMonsterObj.GetComponent<Collider2D>().isTrigger = true;

        return newMonster;
    }

    /* The old factory would create everything by using parameters in code. This allows us to randomly generate hundreds of enemies easily, but makes it
    ** harder to use for non-developers (Game Designers). The new factory system will take advatange of Unity's public editor variables so making enemies is easy
    ** but still very generic to minimize time and knowledge it takes to add a new enemy
    */
    #region Old Factory
    /*
    Dictionary<GV.MonsterTypes, MonsterInfoPair> typeOfMonsterDict;

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

    private MonsterFactory()
    {
        typeOfMonsterDict = new Dictionary<GV.MonsterTypes, MonsterInfoPair>()
        {
            {GV.MonsterTypes.Slime, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Slime, typeof(Slime)   ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Slime, 10, 5,    0f,2))},
            {GV.MonsterTypes.Wisp,  new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Wisp,  typeof(Wisp) ,1, .5f,  1), new SpawnerInfo(GV.MonsterTypes.Wisp,  10, 3.5f, 0f,2))},
            {GV.MonsterTypes.Golem, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Golem, typeof(Monster) ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Golem, 10, 15,   0f,2))},
            {GV.MonsterTypes.Chicken, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Chicken, typeof(Monster) ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Chicken, 10, 3,   0f,2))},
            {GV.MonsterTypes.Bat, new MonsterInfoPair(new MonsterInfo(GV.MonsterTypes.Bat, typeof(Monster) ,1, .25f, 1), new SpawnerInfo(GV.MonsterTypes.Bat, 10, 2,   0f,2))}
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
        if (spawnerInfo.movespeed != 0)
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


    private GameObject CreateMonsterSimple(string monsterName)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Monsters/" + monsterName));
    }

    private GameObject CreateMonsterComplex(GV.MonsterTypes monsterType)
    {
        string monsterName = monsterType.ToString();
        if (!typeOfMonsterDict.ContainsKey(monsterType))
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
    */
    #endregion
}
