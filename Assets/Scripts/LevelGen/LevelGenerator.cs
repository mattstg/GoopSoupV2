using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator  {
    #region singleton
    private static LevelGenerator instance;

    private LevelGenerator() { }

    public static LevelGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelGenerator();
            }
            return instance;
        }
    }
    #endregion

    public void GenerateWorldMap(Vector2 size)
    {
        Grid worldGrid = GV.ws.worldGrid;

        GameObject tileMapObj = new GameObject();
        tileMapObj.name = "TileMap";
        tileMapObj.transform.SetParent(worldGrid.transform);
        tileMapObj.AddComponent<Tilemap>();
        tileMapObj.AddComponent<TilemapRenderer>();

    }

}
