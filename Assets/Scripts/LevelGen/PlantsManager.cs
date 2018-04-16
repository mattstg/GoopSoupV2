using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantsManager {

    #region singleton
    private static PlantsManager instance;

    private PlantsManager() { }

    public static PlantsManager Instance {
        get {
            if (instance == null) 
                instance = new PlantsManager();

            return instance;
        }
    }
    #endregion

    private Dictionary<Vector3Int, Plant> plants = new Dictionary<Vector3Int, Plant>();
    private Sprite[] sprites;

    private Transform parent;

    private Vector3Int selectedPlant;

    public void Init() {
        // Init plants parent
        GameObject go = new GameObject();
        go.name = "Plants";
        parent = go.transform;

        // Init sprites
        sprites = Resources.LoadAll<Sprite>("Sprites/Plants/Plants");

        // Generate flowers
        float generatedFlowers = 0;
        while (generatedFlowers < GV.ws.flowerNumber) {
            Vector2 rand = GV.GetRandomSpotInMap();
            Vector3Int pos = new Vector3Int(Mathf.RoundToInt(rand.x), Mathf.RoundToInt(rand.y), 0);

            if (!plants.ContainsKey(pos)) {
                int spriteIndex = Random.Range(0, sprites.Length);
                CreatePlant(pos, CreateRandomIngredient(), spriteIndex);
                generatedFlowers += 1;
            }
        }
    }

    public void Update (float _dt) {
        for (int index = plants.Count - 1; index >= 0; index--)
            plants.ElementAt(index).Value.UpdatePlant(_dt);
    }

    public void RemovePlant(Vector3Int _position) {
        if (plants.ContainsKey(_position)) {
            GameObject.Destroy(plants[_position].gameObject);
            plants.Remove(_position);
        }
    }

    public void PickupPlant (Transform _plant) {
        selectedPlant = new Vector3Int(
            Mathf.RoundToInt(_plant.transform.position.x), 
            Mathf.RoundToInt(_plant.transform.position.y),
            0
        );
        plants[selectedPlant].isMoving = true;
    }

    public void DropPlant (Transform _plant) {
        Plant plant = plants[selectedPlant];

        plant.isMoving = false;
        plants.Remove(selectedPlant);

        Vector3Int pos = new Vector3Int((int)_plant.transform.position.x, (int)_plant.transform.position.y, 0);
        plant.DropPlant(pos);

        _plant.position = pos;
        _plant.SetParent(parent);

        plants.Add(pos, plant);

        selectedPlant = new Vector3Int(-1, -1, 0);
    }

    public void TryToSpread (Vector3Int _pos, Plant _plant) {
        // Test for world environnement
        RaycastHit2D rh = Physics2D.Raycast(new Vector2(_pos.x, _pos.y), new Vector2(), 1f);

        if (rh && !rh.collider.gameObject.CompareTag("Cauldron")) {
            Plant plant = CheckForPlant(_pos);
            if (plant && plant.id == _plant.id) {
                RemovePlant(_pos);
                CreatePlant(_pos, (plant.ingredient + _plant.ingredient), _plant.id);
            }

            if (!plant) {
                float chanceToMutate = Random.Range(0f, 10f);
                if (chanceToMutate >= 10) {
                    CreatePlant(_pos, SlighlyMutateIngredient(_plant.ingredient), _plant.id);
                } else {
                    CreatePlant(_pos, _plant.ingredient, _plant.id);
                }
            }
        }
    }

    private Plant CheckForPlant(Vector3Int _pos) {
        return plants.ContainsKey(_pos) ? plants[_pos] : null;
    }

    private Ingredient SlighlyMutateIngredient(Ingredient _ingredient) {
        _ingredient.r = Random.Range(0, 2) == 0 ? _ingredient.r += 10 : _ingredient.r -= 10;
        _ingredient.g = Random.Range(0, 2) == 0 ? _ingredient.g += 10 : _ingredient.g -= 10;
        _ingredient.b = Random.Range(0, 2) == 0 ? _ingredient.b += 10 : _ingredient.b -= 10;

        return _ingredient;
    }

    private Ingredient CreateRandomIngredient() {
        return new Ingredient(
            Random.Range(0, 256),
            Random.Range(0, 256),
            Random.Range(0, 256)
        );
    }

    private Sprite GetSprite(int _index) {
        return sprites[_index];
    }

    private void CreatePlant(Vector3Int _pos, Ingredient _ingredient, int _spriteIndex) {
        GameObject plant = new GameObject();
        plant.name = "Plant";

        Plant script = plant.AddComponent<Plant>();

        script.ingredient = _ingredient;

        script.SetSprite(GetSprite(_spriteIndex), _spriteIndex);

        plant.transform.position = _pos;
        plant.transform.SetParent(parent);

        plants.Add(_pos, script);
    }
}