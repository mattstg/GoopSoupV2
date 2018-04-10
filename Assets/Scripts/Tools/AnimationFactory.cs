using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFactory  {

    #region Singleton
    private static AnimationFactory instance;

    private AnimationFactory() { }

    public static AnimationFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimationFactory();
            }
            return instance;
        }
    }
    #endregion
    
    Dictionary<string, Sprite[]> animationDict = new Dictionary<string, Sprite[]>();
    string spriteLocations = "Sprites/Monsters/";


    public void SetupAnimationForMonster(Monster monster)
    {
        string animName = monster.animInfo.spriteName;
        if(string.IsNullOrEmpty(animName))
        {
            Debug.LogError("Sprite name for monster: " + monster.name + " is empty, no animations created");
            return;
        }

        CustomAnim anim = monster.gameObject.AddComponent<CustomAnim>();
        if (animationDict.ContainsKey(animName))
        {
            anim.InitializeCustomAnim(monster.GetComponent<SpriteRenderer>(), animationDict[animName], monster.animInfo.animationSpeed);
        }
        else
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(spriteLocations + animName);
            if(sprites == null || sprites.Length == 0)
            {
                Debug.LogError("Failure to load animation system of name: " + monster.name + " at location: " + spriteLocations + ", does asset exist/sprite slice mode == multiple?");
                GameObject.Destroy(anim);
            }
            else
            {
                animationDict.Add(animName, sprites);
                anim.InitializeCustomAnim(monster.GetComponent<SpriteRenderer>(), animationDict[animName], monster.animInfo.animationSpeed);
            }
        }
    }

}
