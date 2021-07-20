using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    readonly float Potion_Explode_Time = .2f;
    readonly float Potion_Explode_Scale_Max = 2.3f; //sprite is 200x200, so explosion raidus is half

    float timeOfDeath;

    IEnumerator UpdateAnim()
    {
        timeOfDeath = Time.time + Potion_Explode_Time;
        while (timeOfDeath > Time.time)
        {
            float p = 1 - ((timeOfDeath - Time.time) / Potion_Explode_Time);
            transform.localScale = new Vector3(1, 1) * Mathf.Lerp(1, Potion_Explode_Scale_Max, p);
            yield return null;
        }
        GameObject.Destroy(gameObject);
    }
      

    public void InitializeExplosion(Vector2 pos, Ingredient ingr)
    {
        //Color the explosion
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/PotionSplash");
        sr.color = ingr.ToColor();
        sr.sortingLayerName = "Potion";

        //Do dmg calcs
        int expLayerMask = LayerMask.GetMask("Monolith", "Monster");  // (1 << LayerMask.NameToLayer("Monolith") | (1 << LayerMask.NameToLayer("Monster")));
        RaycastHit2D[] rhs = Physics2D.CircleCastAll(transform.position, Potion_Explode_Scale_Max, new Vector2(), 0, expLayerMask);
        foreach (RaycastHit2D rh in rhs)
        {
            
            {
                Monolith m = rh.transform.GetComponent<Monolith>();
                if (m)
                {
                    if (ingr.IngredientsAreSimilar(m.ingredient))
                        m.DestroyMonolith();
                }
            }

            {
                Monster m = rh.transform.GetComponent<Monster>();
                if (m)
                {
                    if (ingr.IngredientsAreSimilar(m.ingredientInfo))
                        m.MonsterDies();
                }

            }
        }
        
        //Anim co-routine
        StartCoroutine(UpdateAnim());
    }


    public static void CreateExplosion(Vector2 loc, Ingredient ingredientType)
    {
        GameObject go = new GameObject();
        
        go.transform.position = loc;
        go.AddComponent<Explosion>().InitializeExplosion(loc,ingredientType);
    }

}
