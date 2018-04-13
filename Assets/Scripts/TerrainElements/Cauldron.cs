using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    SpriteRenderer liquidSr;
    Ingredient cauldronIngredients = new Ingredient();

    public void Initialize()
    {
        liquidSr = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Plant"))
        {
            Plant p = collision.gameObject.GetComponent<Plant>();
            cauldronIngredients += p.ingredient;
            GameObject.Destroy(collision.gameObject);
            Debug.Log("Cauldrion ingredients: " + cauldronIngredients);
            liquidSr.color = cauldronIngredients.ToColor();
        }
    }
        
}
