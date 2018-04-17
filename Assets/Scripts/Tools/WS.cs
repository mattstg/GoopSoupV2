using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS : MonoBehaviour {

    public Grid worldGrid;
    public PolygonCollider2D camBounds;
    public UnityEngine.Tilemaps.TileBase rockTile;
    public AnimationCurve thrownAnim;

    public void Awake()
    {
        GV.ws = this;
    }
}
