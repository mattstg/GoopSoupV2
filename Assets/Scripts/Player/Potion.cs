﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour  {

    public Ingredient ingredient;
    string explosionResourcePath; 

	public void PotionExplodes()
    {
        GameObject.Destroy(gameObject);
        //StartCoroutine(Exploding(transform.position)); --Implemented in later feature
    }

    IEnumerator Exploding(Vector2 loc)
    {
        yield return null;
    }

    public static Potion CreatePotion(Ingredient _ingredient)
    {
        GameObject newPotion = new GameObject();
        newPotion.name = "Potion";
        newPotion.layer = LayerMask.NameToLayer("Potion");
        SpriteRenderer sr = newPotion.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Potion");
        sr.color = _ingredient.ToColor();
        sr.sortingLayerName = "Potion";
        newPotion.AddComponent<BoxCollider2D>().isTrigger = true;
        Potion toRet = newPotion.AddComponent<Potion>();
        toRet.ingredient = new Ingredient(_ingredient);
        return toRet;
    }
}
