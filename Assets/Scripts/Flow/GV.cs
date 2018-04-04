using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum MonsterTypes { Slime, Wisp, Golem, Chicken, Bat }
    public static readonly Vector2 Map_Size_XY = new Vector2(32,17); //NxN map size
    public static readonly float Monster_Breed_SpawnDist = 2; //How far spawns from breeder
    public static readonly float Spawner_Summon_Time = 15; //Attempts to summon a spawner ever N seconds

    public static T GetRandomEnum<T>() where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        List<T> fullEnumList = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
        return fullEnumList[UnityEngine.Random.Range(0, fullEnumList.Count)];
    }

    public static Vector2 GetRandomSpotInMap()
    {
        return new Vector2(UnityEngine.Random.Range(1, Map_Size_XY.x - 1), UnityEngine.Random.Range(1, Map_Size_XY.y - 1));
    }
}
