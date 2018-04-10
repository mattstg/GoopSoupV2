using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFactory : MonoBehaviour {

    #region Singleton
    private static AnimationFactory instance;

    private AnimationFactory() { }

    public static AnimationFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimationFactory();
            }
            return instance;
        }
    }
    #endregion

    public void InitializeAnimations()
    {

    }

}
