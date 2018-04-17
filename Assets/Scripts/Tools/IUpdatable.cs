using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdatable  {
    void Initialize();
    void IUpdate(float dt);
}
