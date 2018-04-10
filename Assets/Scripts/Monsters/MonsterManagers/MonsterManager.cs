using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager {

    #region Singleton
    private static MonsterManager instance;

    public static MonsterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterManager();
            }
            return instance;
        }
    }
    #endregion

    Dictionary<System.Type,List<Monster>> monsterDict;
    Stack<Monster> monstersToRemoveStack;
    Stack<Monster> monstersToAddStack;

    private MonsterManager()
    {
        monsterDict = new Dictionary<System.Type, List<Monster>>();
        monstersToRemoveStack = new Stack<Monster>();
        monstersToAddStack = new Stack<Monster>();
    }

    public void UpdateMonsterManager(float dt)
    {
        //Remove monsters from the dictionary from the RemoveStack
        while (monstersToRemoveStack.Count > 0)
        {
            Monster toRemove  = monstersToRemoveStack.Pop();
            System.Type mType = toRemove.GetType(); //Returns type, for example, slime.cs is has a system type Slime, its base type is Monster. It is the CLASS type
            if(!monsterDict.ContainsKey(mType) || !monsterDict[mType].Contains(toRemove))
            {
                Debug.LogError("Stack tried to remove element of type: " + mType.ToString() + " but was not found in dictionary?");
            }
            else
            {
                monsterDict[mType].Remove(toRemove);
                ObjectPool.Instance.AddToPool(toRemove.name, toRemove);
                if (monsterDict[mType].Count == 0)
                    monsterDict.Remove(mType);
            }
        }


        //Add Monsters to the dictionary from the "toAdd stack"
        while (monstersToAddStack.Count > 0)
        {
            Monster toAdd = monstersToAddStack.Pop();
            System.Type monsterType = toAdd.GetType();

            if (!monsterDict.ContainsKey(monsterType))// || !monsterDict[kv.Key].Contains(kv.Value))
            {
                monsterDict.Add(monsterType, new List<Monster>() { toAdd });
            }
            else if(!monsterDict[monsterType].Contains(toAdd))
            {
                monsterDict[monsterType].Add(toAdd);
            }
            else
            {
                //Spotting an error where the same monster is being initialized twice is almost impossible sometimes
                Debug.LogError("The monster you are trying to add is already in the monster dict"); 
            }
        }


        //monsterSpawner.UpdateMonsterSpawner(dt);
        foreach (KeyValuePair<System.Type,List<Monster>> kv in monsterDict)
            foreach(Monster m in kv.Value)
                m.UpdateMonster(dt);
    }

    public void CreateSpawner(Vector2 loc)
    {
        //monsterSpawner = MonsterFactory.Instance.CreateMonsterSpawner(monsterType.ToStrin, this);
        //monsterSpawner.transform.position = loc;
    }

    public void AddMonster(Monster toAdd)
    {
        monstersToAddStack.Push(toAdd);
    }

    public void RemoveMonster(Monster toRemove)
    {
        monstersToRemoveStack.Push(toRemove);
    }
}
