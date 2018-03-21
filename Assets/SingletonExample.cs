using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample {

    #region Singleton
    private static SingletonExample instance;

    private SingletonExample() { }

    public static SingletonExample Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SingletonExample();
            }
            return instance;
        }
    }
    #endregion

    public void TestFuncA() { }

}
