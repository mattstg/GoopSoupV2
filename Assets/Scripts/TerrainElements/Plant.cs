using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour {
    public Ingredient ingredient;
    
    SpriteRenderer sr;
    int plantNum;

    public void Initialize()
    {
        plantNum = Random.Range(0, 8);
        Debug.Log("Plant number " + plantNum);
        sr = GetComponent<SpriteRenderer>();
        sr.color = ingredient.ToColor();
        sr.sprite = PlantManager.Instance.plantSprite[plantNum];
    }

    public void UpdatePlant(float dt)
    {

    }
}
