using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    public SpriteRenderer liquidSr;
    public Ingredient cauldronIngredients;

    public void Initialize()
    {
        cauldronIngredients = new Ingredient();
        liquidSr.color = cauldronIngredients.ToColor();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Plant"))
        {
            Plant p = collision.gameObject.GetComponent<Plant>();
            cauldronIngredients += p.ingredient;
            PlantManager.Instance.RemovePlant(p);
            GameObject.Destroy(collision.gameObject);
            liquidSr.color = cauldronIngredients.ToColor();
        }
    }
        
}
