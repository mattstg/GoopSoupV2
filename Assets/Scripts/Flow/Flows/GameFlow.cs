using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameFlow : Flow {

	public override void InitializeFlow()
    {
        PlayerManager.Instance.Initialize();
        MonsterFactory.Instance.InitializeFactory();
        TimerManager.Instance.AddTimer(1, TestFuncD, 2f);
    }

    public override void UpdateFlow(float dt)
    {
        PlayerManager.Instance.Update(dt);
        MonsterRandomSpawner.Instance.Update(dt);
        MonsterManager.Instance.UpdateMonsterManager(dt);
    }

    public override void FixedUpdateFlow(float dt)
    {
        PlayerManager.Instance.FixedUpdate(dt);
    }

    public void TestFuncA(float a)
    {

    }

    public void TestFuncB(float a, float b)
    {

    }

    public void TestFuncC(float m, string a, int c, string twelve)
    {

    }

    public void TestFuncD(params object[] args) { }
}
