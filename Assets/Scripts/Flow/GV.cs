using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public static WS ws;

    public static readonly Vector2Int Map_Size_XY = new Vector2Int(32,17); //NxN map size

    public static readonly int Plants_InitialSpawnCount = 20;
    public static readonly Vector2 Plants_Breed_Time_Range = new Vector2(12, 20);
    public static readonly Vector2 Plants_Breed_Distance_Range = new Vector2(1, 3);
    public static readonly float Plants_Breed_Time_CountMultiplier = .01f; //Increases the time to breed by 1% for every plant that exists
    public static readonly float Plants_Breed_Mutation_Chance = .1f;
    public static readonly Vector2 Plants_Breed_Mutation_Variation_Range = new Vector2(.1f,.3f);

    public static readonly float Ingredient_Similarity_Range = .3f; //If plants ingredients within X of each other, are considered similar

    public static readonly float Player_Drop_Distance = .5f;
    public static readonly float Player_Throw_Distance = 1.6f;
    public static readonly float Player_Throw_Time = .5f;

    public static readonly float Monster_Breed_SpawnDist = 2.5f; //How far spawns from breeder
    public static readonly float Monster_Move_TargetReachDist = .1f; //When within this range, it is considered reaching its target

    public static readonly int Monolith_Spawn_Initial = 2; //Number of monoliths in the game start
    public static readonly int Monolith_Spawn_Monolith_Rate = 120; //Number of seconds to spawn a new monolith
    public static readonly int Monolith_Spawn_Monster_Rate = 12; //Every N seconds spawns monster near monolith

    public static readonly bool DEBUG_Ingredients_Always_Similar = true;
    public static readonly bool DEBUG_Monsters_Triggers = false;


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
        //The edges are size 1x1, so take that into account
        return new Vector2(UnityEngine.Random.Range(2, Map_Size_XY.x - 2), UnityEngine.Random.Range(2, Map_Size_XY.y - 2));
    }

    /// <summary>
    /// Returns a random spot near in a unit circle
    /// </summary>    
    public static Vector2 GetRandomSpotNear(Vector2 location, float maxDistance)
    {
        return location + UnityEngine.Random.insideUnitCircle * maxDistance;
    }

    /// <summary>
    /// Returns true if a and b are within acceptableRange of each other
    /// </summary>
    public static bool WithinRange(float a, float b, float acceptableRange)
    {
        return (Mathf.Abs(a - b) <= acceptableRange);
    }

    /// <summary>
    /// Returns random spot near in unit circle with a min distance
    /// </summary>
    /// <param name="location"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public static Vector2 GetRandomSpotNear(Vector2 location, Vector2 distRange)
    {
        Vector2 v = UnityEngine.Random.onUnitSphere;
        Vector2 v2 = Vector2.Lerp(v * distRange.x, v * distRange.y, UnityEngine.Random.value); 
        return location + v2;
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

    public static float GetRandomFromV2(Vector2 v2)
    {
        return UnityEngine.Random.Range(v2.x, v2.y);
    }

    public static float RandomNegator()
    {
        return (UnityEngine.Random.value > .5f) ? 1: -1;
    }

}
