using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    readonly static float Player_Dmg_Invunerable_Time = 2;
    readonly static float Player_Hp_Max = 10;

    bool initialized = false;
    InputManager inputManager;
    Rigidbody2D rb;
    int carryMask;
    Vector2 lastMovedDir;
    //Movement
    readonly float PLAYER_MOVE_VELO = 3;
    float playerHp = Player_Hp_Max;
    float timeAtLastDamage;
    

    //Carry mechanics
    Vector2 localHandPos;
    Carried carryingObject;

    public void PlayerSpawned()
    {
        if (!initialized)
            PlayerFirstInitialize();
        playerHp = Player_Hp_Max;
    }

    public void PlayerFirstInitialize()
    {
        inputManager = new InputManager();
        inputManager.InitializeManager();
        rb = GetComponent<Rigidbody2D>();
        //Created a transform spot for hand, so consume it
        Transform handTransform = transform.Find("HandSpot");
        localHandPos = handTransform.localPosition ; //transform.InverseTransformPoint(handTransform.position)
        GameObject.Destroy(handTransform.gameObject);
        initialized = true;
        carryMask = (1 << LayerMask.NameToLayer("Plant") | 1 << LayerMask.NameToLayer("Cauldron") | (1 << LayerMask.NameToLayer("Potion")));
    }

    public void UpdatePlayer(float dt)
    {
        if (Input.GetKeyDown(KeyCode.I))
            HealthPopupManager.Instance.ModHP(transform, 2.5f);

        inputManager.UpdateManager(dt);
        InputManager.InputInfo inputInfo = inputManager.GetInputInfo();
        lastMovedDir = (inputInfo.dirPressed == new Vector2()) ? lastMovedDir : inputInfo.dirPressed ;

        if (inputInfo.pickDropPressed)
            if (carryingObject)
                DroppedPresed();
            else
                PickupPressed();

        if (inputInfo.makePotionPressed)
            if (carryingObject)
            {
                GameObject toThrow = DeattachCarryingItem();
                StartCoroutine(ThrowCarriedObject(toThrow, lastMovedDir.normalized*GV.Player_Throw_Distance,GV.Player_Throw_Time));
            }
            else
                MakePotionPressed();
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = dir.normalized * PLAYER_MOVE_VELO;
    }

    private RaycastHit2D RaycastOutFromHand(float dist, int mask)
    {
        return Physics2D.Raycast((Vector2)transform.position + localHandPos, lastMovedDir, dist, carryMask);
    }

    private void PickupPressed()
    {
        RaycastHit2D rh = RaycastOutFromHand(.33f, carryMask);
        if (rh)
            PickupObject(rh.transform.gameObject);
    }

    //Would be better if when we pickedup an object, we attached a  "carried" script to it, that way
    //reseting back to the proper values is easier
    private void PickupObject(GameObject toPickUp)
    {
        carryingObject = Carried.PickUpObject(toPickUp, transform, localHandPos);
        //toPickUp.transform.SetParent(transform);
        //toPickUp.transform.localPosition = localHandPos;
        //carryingObject = toPickUp;
        //Collider2D coli = toPickUp.GetComponent<Collider2D>();
        //Rigidbody2D rb = toPickUp.GetComponent<Rigidbody2D>();
        //if(coli) coli.enabled = false;
        //if (rb) rb.bodyType = RigidbodyType2D.Kinematic;
        //isCarrying = true;
        
    }

    IEnumerator ThrowCarriedObject(GameObject toThrow, Vector2 displacement, float throwTime)
    {
        Vector2 startPos = toThrow.transform.position;
        Vector2 endPos = startPos + displacement;
        Vector2 startScale = toThrow.transform.localScale;
        bool verticalDisplacement = (displacement.y != 0);
        float endTime = Time.time + throwTime;
        //See, more examples where turning off and on colliders everywhere! Should just be a "carried/thrown" script!
        Collider2D coli = toThrow.GetComponent<Collider2D>();
        if(coli) coli.enabled = false;
        while(Time.time < endTime)
        {
            float perctenage = 1 - ((endTime - Time.time) / throwTime);
            if (!verticalDisplacement)
                toThrow.transform.position = new Vector2(Mathf.Lerp(startPos.x, endPos.x, perctenage), GV.ws.thrownAnim.Evaluate(perctenage) + startPos.y);
            else
            {
                toThrow.transform.position = Vector2.Lerp(startPos, endPos, perctenage);
                toThrow.transform.localScale = Vector2.Lerp(startScale,startScale*2,GV.ws.thrownAnim.Evaluate(perctenage));
            }
            yield return null;
        }
        Potion p = toThrow.GetComponent<Potion>();
        if (p)
            p.PotionExplodes();
        else if (toThrow.GetComponent<Collider2D>())
            toThrow.GetComponent<Collider2D>().enabled = true;
    }

    public void TakeDamage(float dmg)
    {
        if (Time.time > timeAtLastDamage + Player_Dmg_Invunerable_Time)
        {
            playerHp -= dmg;
            HealthPopupManager.Instance.ModHP(transform, dmg);
            if (playerHp <= 0)
                PlayerManager.Instance.PlayerDied();
            timeAtLastDamage = Time.time;
        }
    }

    private void MakePotionPressed()
    {
        //So many raycasts checking for a direction, could be made more generic by having one location for all checks
        RaycastHit2D rh = RaycastOutFromHand(1, (1 << LayerMask.NameToLayer("Cauldron")));
        if (rh && rh.transform.GetComponent<Cauldron>())
        {
            Potion newPotion = Potion.CreatePotion(rh.transform.GetComponent<Cauldron>().cauldronIngredients);
            PickupObject(newPotion.gameObject);
        }
    }

    private void DroppedPresed()
    {
        if (carryingObject)
        {
            GameObject droppedObj = DeattachCarryingItem();
            droppedObj.transform.localPosition = droppedObj.transform.localPosition + (Vector3)(lastMovedDir.normalized * GV.Player_Drop_Distance);
        }

    }

    //Would be better if there was an "attached" and "deattached" script on object, simpler to remove
    //and reset things back to original, here we assume the rigidbody should be dynamic... dangerous
    private GameObject DeattachCarryingItem()
    {
        //GameObject toRet = carryingObject;
        //Collider2D coli = toRet.GetComponent<Collider2D>();
        //Rigidbody2D rb = toRet.GetComponent<Rigidbody2D>();
        //if (coli) coli.enabled = true;
        //if (rb) rb.bodyType = RigidbodyType2D.Dynamic;
        //isCarrying = false;
        //carryingObject.transform.SetParent(null); //This is bad, what if it belonged to a parent group?
        //carryingObject = null;
        //return toRet;

        GameObject toRet = carryingObject.gameObject;
        carryingObject.ObjectDropped();
        carryingObject = null;
        return toRet;
    }

    public void FixedUpdatePlayer(float dt)
    {
        InputManager.InputInfo inputInfo = inputManager.GetInputInfo();
        Move(inputInfo.dirPressed);
    }

    public void PlayerDied()
    {
        PlayerManager.Instance.PlayerDied();
    }
	
    //Since using object pooling, just reset the player
    public void ResetPlayer()
    {

    }
}
