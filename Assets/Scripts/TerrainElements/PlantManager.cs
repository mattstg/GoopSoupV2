using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager {

    #region Singleton
    private static PlantManager instance;

    private PlantManager() { }

    public static PlantManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlantManager();
            }
            return instance;
        }
    }
    #endregion

    public Plant plant { private set; get; }
    public Sprite[] plantSprite;

    public void Initialize()
    {
        plantSprite = Resources.LoadAll<Sprite>("Sprites/Plants");
        Vector2 plantPos = GV.GetRandomSpotInMap();
        SpawnPlant(plantPos);
    }

    public void Update(float dt)
    {
        if (plant)
            plant.UpdatePlant(dt);
    }

    public void SpawnPlant(Vector2 location)
    {
        if (!plant)
        {
            GameObject plantObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Plant"));
            plant = plantObj.GetComponent<Plant>();
        }
        plant.transform.position = location;
        plant.Initialize();
    }
}
