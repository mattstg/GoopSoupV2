using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS : MonoBehaviour {

    public Grid worldGrid;
    public PolygonCollider2D camBounds;

    public void Awake()
    {
        GV.ws = this;
    }
}
