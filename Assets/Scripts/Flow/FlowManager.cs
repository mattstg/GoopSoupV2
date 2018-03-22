using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager  {
    public enum SceneNames { MainMenu, MainScene }

    #region Singleton
    private static FlowManager instance;

    private FlowManager() { }

    public static FlowManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FlowManager();
            }
            return instance;
        }
    }
    #endregion

    public SceneNames currentScene;

    public void InitializeFlowManager(SceneNames initialScene)
    {
        currentScene = initialScene;
    }

    public void Update(float dt)
    {

    }
}
