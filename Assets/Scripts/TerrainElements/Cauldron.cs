using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    SpriteRenderer liquidSr;
    Ingredient cauldronIngredients = new Ingredient();

    public void Initialize() {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
            if (renderer.gameObject.transform.parent != null)
                liquidSr = renderer;
    }

	public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Plant")) {
            Plant p = collision.gameObject.GetComponent<Plant>();
            cauldronIngredients += p.ingredient;
            // GameObject.Destroy(collision.gameObject);
            PlantsManager.Instance.RemovePlant(collision.gameObject.GetComponent<Plant>().basePos);
            PlayerManager.Instance.player.isCarrying = false;
            Debug.Log("Cauldrion ingredients: " + cauldronIngredients);
            liquidSr.color = cauldronIngredients.ToColor();
        }
    }
        
}
