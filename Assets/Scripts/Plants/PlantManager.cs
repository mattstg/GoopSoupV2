
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager  {
    #region singleton
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

    Transform plantGroup;
    Sprite[] plantSprites;
    List<Plant> plants;
    public float PlantCount { get { return plants.Count; } }
    public static int plantPlacementLayerCheck;

    public void Initialize()
    {
        plantSprites = Resources.LoadAll<Sprite>("Sprites/Plants");
        plants = new List<Plant>();
        plantGroup = new GameObject().transform;
        plantGroup.name = "PlantGroup";
        plantPlacementLayerCheck = (1 << LayerMask.NameToLayer("Plant") | 1 << LayerMask.NameToLayer("Cauldron") | 1 << LayerMask.NameToLayer("MapEdges"));
    }

    public void FillWorldWithPlants(int numOfPlants)
    {
        //I want to have one of each unique type first
        for(int i = 0; i < numOfPlants; i++)
        {
            Ingredient ingr = Ingredient.RandomIngredient();
            Sprite plantSprite = (i < plantSprites.Length)? plantSprites[i]:plantSprites[Random.Range(0,plantSprites.Length)];
            GameObject go = new GameObject();
            Plant newPlant = go.AddComponent<Plant>();
            newPlant.InitializePlant(ingr, plantSprite);
            Vector2 plantLoc = GetValidPlantLocation(newPlant.coli.bounds.size);
            if(plantLoc == new Vector2(-1,-1))
            {//No place found for plant
                GameObject.Destroy(newPlant.gameObject);
            }
            else
            {
                newPlant.transform.position = plantLoc;
                plants.Add(newPlant);
            }            
        }
    }

    public void CreateNewPlant(Vector2 loc, Ingredient ingredients, Sprite sprite)
    {
        GameObject newPlant = new GameObject();
        newPlant.name = "Plant";
        Plant plantScript = newPlant.AddComponent<Plant>();
        plantScript.InitializePlant(ingredients, sprite);
        newPlant.transform.SetParent(plantGroup);
        newPlant.transform.position = loc;
        plants.Add(plantScript);
    }

    public void RemovePlant(Plant toRemove)
    {
        plants.Remove(toRemove);
    }

    private Vector2 GetValidPlantLocation(Vector2 plantSize)
    {
        int attempts = 0;
        while(attempts < 100)
        {
            Vector2 randPlace = GV.GetRandomSpotInMap();
            randPlace = new Vector2((int)randPlace.x, (int)randPlace.y); //making the map more like a grid
            RaycastHit2D rh = Physics2D.BoxCast(randPlace, plantSize, 0, new Vector2(),0, plantPlacementLayerCheck);
            if (!rh)
                return randPlace;
            attempts++;
        }
        Debug.Log("No place found for plant, returning null");
        return new Vector2(-1,-1);
    }

    public void UpdatePlants(float dt)
    {
        for(int i = plants.Count - 1; i >= 0; i--)
            if(plants[i])
                plants[i].UpdatePlant(dt);
    }


}
