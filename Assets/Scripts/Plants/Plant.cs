using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    public Ingredient ingredient;
    public Sprite sprite;
    [HideInInspector] public Collider2D coli;
    [HideInInspector] public SpriteRenderer sr;

    float spreadCountdown;

    public void InitializePlant(Ingredient _ingredient, Sprite _sprite)
    {
        ingredient = new Ingredient(_ingredient); //Clone it so dont all share
        gameObject.layer = LayerMask.NameToLayer("Plant");
        gameObject.tag = "Plant";
        gameObject.name = "Plant";

        sprite = _sprite;
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerName = "Plant";
        sr.color = ingredient.ToColor();

        coli = gameObject.AddComponent<BoxCollider2D>();
        coli.isTrigger = true;

        SetSpreadAttemptCountdown();
    }

    public void MergeWithIncomingPlant(Ingredient ingr)
    {
        ingredient += ingr;
        sr.color = ingredient.ToColor();
        SetSpreadAttemptCountdown();
    }

    private void SetSpreadAttemptCountdown()
    {
        spreadCountdown = GV.GetRandomFromV2(GV.Plants_Breed_Time_Range) * (1 + (GV.Plants_Breed_Time_CountMultiplier * PlantManager.Instance.PlantCount));
    }

    public void UpdatePlant(float dt)
    {
        spreadCountdown -= dt;
        if(spreadCountdown <= 0)
        {
            AttemptSpread();
            SetSpreadAttemptCountdown();
        }
    }

    private void AttemptSpread()
    {
        Vector2 randPlace = GV.GetRandomSpotNear(transform.position, GV.Plants_Breed_Distance_Range);
        randPlace = new Vector2((int)randPlace.x, (int)randPlace.y);
        RaycastHit2D rh = Physics2D.BoxCast(randPlace, coli.bounds.size, 0, new Vector2(), 0, PlantManager.plantPlacementLayerCheck);
        if (!rh)
            SpreadPlant(randPlace);
        else
        {
            Plant otherPlant = rh.transform.GetComponent<Plant>();
            if (otherPlant && IsBreedable(otherPlant))
                otherPlant.MergeWithIncomingPlant(ingredient);
        }
    }

    //Create a new plant at location
    private void SpreadPlant(Vector2 loc)
    {
        Ingredient toClone = new Ingredient(ingredient);
        if (Random.value > GV.Plants_Breed_Mutation_Chance)
            toClone.Mutate();
        PlantManager.Instance.CreateNewPlant(loc, toClone, sprite);
    }

    private bool IsBreedable(Plant otherPlant)
    {
        if (sprite != otherPlant.sprite)
            return false; //Cannot breed plants of two different sprites
        if (ingredient.IngredientsAreSimilar(otherPlant.ingredient))
            return false;
        return true;
    }

}
