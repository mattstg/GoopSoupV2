using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool  {

    Stack<GameObject> pooledObjects = new Stack<GameObject>();

	public void AddToPool(GameObject gameObject)
    {

    }

    public GameObject RetrieveFromPool()
    {
        return null;
    }
}
