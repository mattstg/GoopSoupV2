using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carried : MonoBehaviour {

    Collider2D coli;
    Rigidbody2D rb;
    Transform originalParent;
    bool initialColiState;
    RigidbodyType2D initialBodyType;

    public void ObjectPickedUp()
    {
        originalParent = transform.parent;
        coli = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        if (coli)
        {
            initialColiState = coli.enabled;
            coli.enabled = false;
        }
        if(rb)
        {
            initialBodyType = rb.bodyType;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2();
        }
    }

    public void ObjectDropped()
    {
        transform.SetParent(originalParent);
        if (coli)
            coli.enabled = initialColiState;
        if (rb)
            rb.bodyType = initialBodyType;
        Destroy(this);
    }

    public static Carried PickUpObject(GameObject toCarry, Transform toFollow, Vector2 offset)
    {
        Carried toRet = toCarry.AddComponent<Carried>();
        toRet.ObjectPickedUp();
        toRet.transform.SetParent(toFollow);
        toRet.transform.localPosition = offset;
        return toRet;
    }
}
