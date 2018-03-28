using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool  {
    
    #region
    private static ObjectPool instance;

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
    Dictionary<string, Stack<IPoolable>> pooledObjects = new Dictionary<string, Stack<IPoolable>>();

    private ObjectPool()
    {
        objectPoolParent = new GameObject().transform;
        objectPoolParent.name = "ObjectPool";
    }

    public void AddToPool(string objName, IPoolable poolable)
    {
        if (!pooledObjects.ContainsKey(objName))
            pooledObjects.Add(objName, new Stack<IPoolable>());
        pooledObjects[objName].Push(poolable);
        poolable.GetGameObject.transform.SetParent(objectPoolParent);
        poolable.GetGameObject.SetActive(false);
        poolable.Pooled(); //Just in case the object wants to be alerted

    }

    public IPoolable RetrieveFromPool(string objectName)
    {
        if (pooledObjects.ContainsKey(objectName) && pooledObjects[objectName].Count > 0)
        {
            IPoolable toRet = pooledObjects[objectName].Pop();
            toRet.GetGameObject.transform.SetParent(null);
            toRet.GetGameObject.SetActive(false);
            toRet.DePooled();  //Just in case the object wants to be alerted
            return toRet;
        }
        return null;
    }
}
