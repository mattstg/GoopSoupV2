using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterObjectPool  {

    #region Singleton
    private static MasterObjectPool instance;

    private MasterObjectPool() { }

    public static MasterObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MasterObjectPool();
            }
            return instance;
        }
    }
    #endregion

    public Dictionary<string, ObjectPool> masterObjectPoolDict = new Dictionary<string, ObjectPool>();

    public void AddObject(string objectType, GameObject toAdd)
    {
        if(masterObjectPoolDict.ContainsKey(objectType))
        {
            masterObjectPoolDict[objectType].AddToPool(toAdd);
        }
        else
        {
            ObjectPool newPool = new ObjectPool();
            newPool.AddToPool(toAdd);
        }
    }

    public GameObject GetObjectFromPool(string objectType)
    {
        if (masterObjectPoolDict.ContainsKey(objectType))
            return masterObjectPoolDict[objectType].RetrieveFromPool();
        else
            return null;
    }
}
