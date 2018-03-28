using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory  {
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

    /*
    To highlight the different kinds of factory, we would normally only implement one type
    
        Simplest
        Creates the object by directly loading the prefab

        Complex
        Doesn't use prefabs, creates an object by assembling all the required parts. Retrieving the Monster script from the TypeOfMonsterDict we create.
        
        Advanced
        Assembles all the required parts like complex. However, it does not require a TypeOfMonsterDict. Instead, it gathers all the .cs files inside "Monster" folder, converts
        all the files it finds there (since the files are classes) into System.Types (Like Complex). This method allows us not to even require enums. All monsters would be auto generated
        by whatever scripts are available in the folder
        
    */
    enum FactoryType {  Simplest, Complex, Advanced }
    FactoryType factoryMode = FactoryType.Complex;

    //Used for storing System.Types, can be used to AddComponents to objects
    Dictionary<string, System.Type> typeOfMonsterDict;

    private MonsterFactory()
    {
        typeOfMonsterDict = new Dictionary<string, System.Type>()
        {
            {"Slime", typeof(Slime)}
        };
    }


    public Monster CreateMonster(string monsterName)
    {
        GameObject toRetObj = null;
        IPoolable poolable = ObjectPool.Instance.RetrieveFromPool(monsterName);
        if (poolable != null)
            toRetObj = poolable.GetGameObject;

        if (!toRetObj)
        {
            switch (factoryMode)
            {
                case FactoryType.Simplest:
                    toRetObj = CreateMonsterSimple(monsterName);
                    break;
                case FactoryType.Complex:
                    toRetObj = CreateMonsterComplex(monsterName);
                    break;
                case FactoryType.Advanced:
                    toRetObj = CreateMonsterAdvanced(monsterName);
                    break;
                default:
                    toRetObj = CreateMonsterSimple(monsterName);
                    Debug.LogError("Switch case unhandled: " + factoryMode);
                    break;
            }
        }

        Monster toRet = toRetObj.GetComponent<Monster>();
        if(!toRet)
            Debug.LogError("Something went wrong in monster factory, object: " + toRetObj.name + " did not contain a monster script. Returning Null");
        return toRet;
    }

    private GameObject CreateMonsterSimple(string monsterName)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Monsters/" + monsterName));
    }

    private GameObject CreateMonsterComplex(string monsterName)
    {
        if(!typeOfMonsterDict.ContainsKey(monsterName))
        {
            Debug.LogError("CreateMonsterComplex could not find monster: " + monsterName + " in the typeOfMonsterDict");
            return null;
        }
        GameObject newMonster = new GameObject();
        newMonster.name = monsterName;
        newMonster.tag = "Monster";
        newMonster.layer = LayerMask.NameToLayer("Monster");
        newMonster.AddComponent(typeOfMonsterDict[monsterName]);
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
        //This will use the auto class generator which will be covered at a later date
        return null;
    }

}
