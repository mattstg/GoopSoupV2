using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum MonsterTypes { Slime, Bat }
    public static readonly float Map_Size_XY = 32; //NxN map size
    public static readonly float Monster_Breed_Time = 5;
    public static readonly float Monster_Breed_SpawnDist = 2; //How far spawns from breeder


    public static T GetRandomEnum<T>() where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        List<T> fullEnumList = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
        return fullEnumList[UnityEngine.Random.Range(0, fullEnumList.Count)];
    }
}
