using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {

	public override void InitializeFlow()
    {
        PlayerManager.Instance.Initialize();
    }

    public override void UpdateFlow(float dt)
    {
        PlayerManager.Instance.Update(dt);
    }

    public override void FixedUpdateFlow(float dt)
    {
        PlayerManager.Instance.FixedUpdate(dt);
    }
}
