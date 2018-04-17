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

    string grassTileMapPath = "Tilemap/Tiles/Grass";

    public void GenerateWorldMap(Vector2Int size)
    {
        Grid worldGrid = GV.ws.worldGrid;

        { //Putting in scope so dont have to rename vars
            GameObject tileMapObj = new GameObject();
            tileMapObj.name = "TileMap";
            tileMapObj.transform.SetParent(worldGrid.transform);
            Tilemap tilemap = tileMapObj.AddComponent<Tilemap>();
            tileMapObj.AddComponent<TilemapRenderer>();

            //Create tilebase array
            TileBase[] allGrassTiles = Resources.LoadAll<TileBase>(grassTileMapPath); //load all 

            TileBase[] tileArray = new TileBase[size.x * size.y];
            for (int index = 0; index < tileArray.Length; index++)
            {
                tileArray[index] = allGrassTiles[Random.Range(0, allGrassTiles.Length)];
            }

            //Create the positions array
            Vector3Int[] positions = new Vector3Int[size.x * size.y];
            int curIndex = 0;
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                {
                    positions[curIndex] = new Vector3Int(x, y, 0);
                    curIndex++;
                    //If it is a wall, we can overwrite the tileArray
                }

            //Set the tilemap
            tilemap.SetTiles(positions, tileArray);
        }
        //////////////////////////////////////////////////////////////////////////////////////
        //////=====Now we add a second tile map, just for the colliders on the edges=====/////
        //////////////////////////////////////////////////////////////////////////////////////

        { //Putting in scope so dont have to rename vars
            GameObject tileMapObj = new GameObject();
            tileMapObj.name = "MapEdges";
            tileMapObj.layer = LayerMask.NameToLayer("MapEdges");
            tileMapObj.transform.SetParent(worldGrid.transform);
            Tilemap tilemap = tileMapObj.AddComponent<Tilemap>();
            tileMapObj.AddComponent<TilemapRenderer>();

            //Create tilebase array
            TileBase wallTile = GV.ws.rockTile;// Resources.Load<TileBase>(wallTileBasePath); //load all 

            TileBase[] tileArray = new TileBase[size.x * size.y];
            for (int index = 0; index < tileArray.Length; index++)
            {
                tileArray[index] = null;
            }

            //Create the positions array
            Vector3Int[] positions = new Vector3Int[size.x * size.y];
            int curIndex = 0;
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                {
                    positions[curIndex] = new Vector3Int(x, y, 0);
                    if (x == 0 || y == 0 || x == size.x - 1 || y == size.y - 1)
                        tileArray[curIndex] = wallTile;
                    curIndex++;
                    //If it is a wall, we can overwrite the tileArray
                }

            //Set the tilemap
            tilemap.SetTiles(positions, tileArray);

            //add colliders

            TilemapCollider2D tc = tilemap.gameObject.AddComponent<TilemapCollider2D>();
            CompositeCollider2D cc = tilemap.gameObject.AddComponent<CompositeCollider2D>();
            tc.usedByComposite = true;
            tilemap.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        //Setup the cam bounds
        GV.ws.camBounds.points = new Vector2[] { new Vector2(size.x, size.y), new Vector2(0, size.y), new Vector2(0, 0), new Vector2(size.x, 0) };

    }

}
