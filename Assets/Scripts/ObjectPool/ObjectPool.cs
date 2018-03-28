using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool  {
    
    #region
    private static ObjectPool instance;

    private ObjectPool() { }

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    #endregion

    Transform objectPoolParent;
    Dictionary<string, List<GameObject>> pooledObjects;

    	public void AddToPool(GameObject gameObject)
    {

    }

    public GameObject RetrieveFromPool()
    {
        return null;
    }
}
