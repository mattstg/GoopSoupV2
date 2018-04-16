using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient  {
    public float r, g, b;

    public Ingredient()
    {
        r = g = b = (255 / 2f);
    }

    public Ingredient(float _r, float _g, float _b) 
    {
        r = _r;
        g = _g;
        b = _b;
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

    public static Ingredient operator +(Ingredient v1, Ingredient v2)
    {
        //It averages the two of them
        return new Ingredient((v1.r + v2.r)/2, (v1.g + v2.g)/2, (v1.b + v2.b)/2);
    }

}

