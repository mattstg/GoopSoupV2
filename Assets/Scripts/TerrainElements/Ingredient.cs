using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient  {
    public float r, g, b;

    public Ingredient()
    {
        r = g = b = .5f;
    }

    public Ingredient(Ingredient toCopy)
    {
        r = toCopy.r;
        g = toCopy.g;
        b = toCopy.b;
    }

    public Ingredient(float _r, float _g, float _b) 
    {
        r = _r;
        g = _g;
        b = _b;
    }

    public void Mutate()
    {
        r = Mathf.Clamp01(r + GV.GetRandomFromV2(GV.Plants_Breed_Mutation_Variation_Range) * GV.RandomNegator());
        g = Mathf.Clamp01(g + GV.GetRandomFromV2(GV.Plants_Breed_Mutation_Variation_Range) * GV.RandomNegator());
        b = Mathf.Clamp01(b + GV.GetRandomFromV2(GV.Plants_Breed_Mutation_Variation_Range) * GV.RandomNegator());
    }

    public static Ingredient RandomIngredient()
    {
        return new Ingredient(Random.value, Random.value, Random.value);
    }

    //Override string operator so easy to output and test
    public override string ToString()
    {
        return string.Format("(r{0},g{1},b{2})", r, g, b);
    }

    public Color ToColor()
    {
        return new Color(r,g,b);
    }

    /// <summary>
    /// Returns true if all ingredients of the plant are similar
    /// </summary>
    public bool IngredientsAreSimilar(Ingredient otherIngredient)
    {
        if (GV.DEBUG_Ingredients_Always_Similar)
            return true;

        if (GV.WithinRange(r, otherIngredient.r, GV.Ingredient_Similarity_Range)
            && GV.WithinRange(g, otherIngredient.g, GV.Ingredient_Similarity_Range)
            && GV.WithinRange(b, otherIngredient.b, GV.Ingredient_Similarity_Range))
            return true;
        return false;
    }

    public static Ingredient operator +(Ingredient v1, Ingredient v2)
    {
        //It averages the two of them
        return new Ingredient((v1.r + v2.r)/2, (v1.g + v2.g)/2, (v1.b + v2.b)/2);
    }

    

}

