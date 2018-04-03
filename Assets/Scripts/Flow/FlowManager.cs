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
    Flow currentFlow;

    public void InitializeFlowManager(SceneNames initialScene)
    {
        currentScene = initialScene;
        currentFlow = CreateFlow(initialScene);
        currentFlow.InitializeFlow();
    }

    public void Update(float dt)
    {
        if(currentFlow != null)
            currentFlow.UpdateFlow(dt);
    }

    public void FixedUpdate(float dt)
    {
        if (currentFlow != null)
            currentFlow.FixedUpdateFlow(dt);
    }

    private Flow CreateFlow(SceneNames _flowToLoad)
    {
        Flow toRet;
        switch (_flowToLoad)
        {
            case SceneNames.MainMenu:
                toRet = new MainMenuFlow();
                break;
            case SceneNames.MainScene:
                toRet = new GameFlow();
                break;
            default:
                toRet = new GameFlow();
                Debug.LogError("Unhandled Switch: " + _flowToLoad.ToString());
                break;
        }
        return toRet;
    }

    public void SceneLoaded(UnityEngine.SceneManagement.Scene sceneLoaded, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
    {
        //Not used at the moment, but example of an event registered to listen to scene change
        Debug.Log("Scene: " + sceneLoaded.name + " finished loading");
    }
}
