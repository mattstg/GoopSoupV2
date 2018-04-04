using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : Monster {


    public override void Initialize()
    {
        base.Initialize();
        GetComponent<Collider2D>().isTrigger = true;
    }

}
