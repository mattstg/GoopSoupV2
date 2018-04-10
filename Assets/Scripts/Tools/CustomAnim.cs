using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnim : MonoBehaviour {

    int curFrame;           //Current frame
    float clipLength;       //Length of entire clip
    float keyLength;        //Length of a single sprite
    SpriteRenderer sr;      //The sprite renderer
    Sprite[] sprites;       //Reference to the array of sprites
    Coroutine runningCR;    //So we can stop it
    bool wasInitialized;    //Calling AddComponent calls OnEnable, which happens BEFORE we run InitializeCustomAnim. So this bool helps

	public void InitializeCustomAnim(SpriteRenderer _sr, Sprite[] _sprites, float _keyLength)
    {
        curFrame = 0;
        keyLength = _keyLength;
        sprites = _sprites;
        sr = _sr;
        clipLength = keyLength * sprites.Length;
        wasInitialized = true;
        OnEnable();
    }

    public void OnEnable()
    {
        if(wasInitialized)
            runningCR = StartCoroutine(RunAnimation()); //Co routines can only run when object is enabled
    }

    public void OnDisable()
    {
        if (wasInitialized)
            StopCoroutine(runningCR); //No point running when object is disabled, aka object pool
    }


    IEnumerator RunAnimation()
    {
        while(true)
        {
            curFrame = (curFrame + 1) % sprites.Length;
            sr.sprite = sprites[curFrame];
            yield return new WaitForSeconds(keyLength); 
        }
    }
}
