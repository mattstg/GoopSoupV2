using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS : MonoBehaviour {

    public Grid worldGrid;

    public void Awake()
    {
        GV.ws = this;
    }
}
