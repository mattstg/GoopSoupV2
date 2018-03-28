using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable {
    void Pooled();
    void DePooled();
	GameObject GetGameObject { get; }
}
