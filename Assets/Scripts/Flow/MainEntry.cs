using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour {

    //There should only ever be one of these

    public void Awake()
    {
        GV.ws = GameObject.FindObjectOfType<WS>(); //We gotta setup all the links first.

        //System level things
        InputManager.Instance.Initialize();
        ObjectPool.Instance.Initialize();

        //World things
        LevelGenerator.Instance.GenerateWorldMap(GV.Map_Size_XY);
        PlayerManager.Instance.Initialize();
        MonsterFactory.Instance.Initialize();
        PlantManager.Instance.Initialize();
        MonolithManager.Instance.Initialize();


    }

    public void Start()
    {
        //Later initialization could be called here, but we have none to call
    }


    public void Update()
    {
        InputManager.Instance.Update();
        PlayerManager.Instance.Update();
        MonsterManager.Instance.Update();
        PlantManager.Instance.Update();
        MonolithManager.Instance.Update();
    }

    public void FixedUpdate()
    {
        //FlowManager.Instance.FixedUpdate(Time.fixedDeltaTime);
        PlayerManager.Instance.FixedUpdate();
    }


}
