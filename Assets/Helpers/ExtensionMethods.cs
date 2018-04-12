using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

    public static void ResetTransformation(this Transform trans)
    {
        trans.position = new Vector3();
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static Vector3 MultipleVector(this Vector3 thisV, Vector3 otherV)
    {
        return new Vector3(thisV.x *= otherV.x, thisV.y *= otherV.y, thisV.z *= otherV.z);
    }

    public static void AddIngredient(this Ingredient thisIngredient, Ingredient otherIngredient)
    {
        thisIngredient.r += otherIngredient.r;
        thisIngredient.g += otherIngredient.g;
        thisIngredient.b += otherIngredient.b;
    }
}
