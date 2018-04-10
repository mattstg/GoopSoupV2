using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public static readonly Vector2 Map_Size_XY = new Vector2(32,17); //NxN map size

    public static readonly float Monster_Breed_SpawnDist = 2; //How far spawns from breeder
    public static readonly float Monster_Move_TargetReachDist = .1f; //When within this range, it is considered reaching its target

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

    public static Vector2 GetRandomSpotNear(Vector2 location, float maxDistance)
    {
        return UnityEngine.Random.insideUnitCircle * maxDistance;
    }

    /// <summary>
    /// Returns a random element from a generic array.
    /// </summary>
    public static T GetRandomElemFromArr<T>(T[] arr, bool allowNull = false)
    {
        //In a perfect world, the array does not have null elements. We do not live in that world. Tool safety with error logging
        if(arr == null || arr.Length == 0)
        {
            Debug.LogError("Error, GetRandomElemFromArr was passed a null or empty array, returning a default t");
            return (T)Activator.CreateInstance(typeof(T));  //This creates a default instance, for example, if an int, 0, if a class, that classes default constructor, this is not gaureteened to work, this is an attempted catch
        }

        int length = arr.Length;
        int randIndex = UnityEngine.Random.Range(0, length);
        if (allowNull || arr[randIndex] != null)
            return arr[randIndex];
        else
        {
            for(int attempts = 0; attempts < 100; attempts++)
            {
                T toRet = arr[randIndex];
                if(toRet != null)
                    return toRet;
                else
                    randIndex = UnityEngine.Random.Range(0, length);
            }
            Debug.LogError("Get random elem from array could not randomly find a non-null element after many attempts, returning the first element, which may be null");
            return arr[0];
        }
    }

}
