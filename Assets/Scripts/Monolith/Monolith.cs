using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolith : MonoBehaviour, IUpdatable
{
    Ingredient ingredient;
    public SpriteRenderer symbols;
    string monsterType;
    float timeOfNextMonsterSpawn;

    public void Initialize()
    {
        ingredient = Ingredient.RandomIngredient();
        symbols.color = ingredient.ToColor();
        monsterType = MonsterFactory.Instance.GetRandomMonsterName();
    }

    public void IUpdate(float dt)
    {
        if(timeOfNextMonsterSpawn <= Time.time)
        {
            MonsterFactory.Instance.CreateMonster(monsterType, GV.GetRandomSpotNear(transform.position, new Vector2(1, GV.Monster_Breed_SpawnDist)));
            timeOfNextMonsterSpawn = Time.time + GV.Monolith_Spawn_Monster_Rate;
        }
    }

    public static Monolith SpawnRandomMonolith()
    {
        GameObject monoObj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Monolith/Monolith"));
        Monolith toRet = monoObj.GetComponent<Monolith>();
        toRet.Initialize();
        monoObj.transform.position = GV.GetRandomSpotInMap();
        return toRet;
    }
}
