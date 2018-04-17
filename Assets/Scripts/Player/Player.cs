using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool initialized = false;
    InputManager inputManager;
    Rigidbody2D rb;
    int carryMask;
    //Movement
    readonly float PLAYER_MOVE_VELO = 3;

    //Carry mechanics
    bool isCarrying = false;
    Vector2 localHandPos;
    GameObject carryingObject;

    public void PlayerSpawned()
    {
        if (!initialized)
            PlayerFirstInitialize();
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

        if (inputInfo.pickDropPressed)
            if (isCarrying)
                DroppedPresed();
            else
                PickupPressed();

        if (inputInfo.makePotionPressed)
            if (isCarrying)
            {
                GameObject toThrow = DeattachCarryingItem();
                StartCoroutine(ThrowCarriedObject(toThrow, GV.Player_Throw_Distance,GV.Player_Throw_Time));
                
            }
            else
                MakePotionPressed();
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = dir.normalized * PLAYER_MOVE_VELO;
    }

    private void PickupPressed()
    {
        RaycastHit2D rh = Physics2D.Raycast((Vector2)transform.position + localHandPos, new Vector2(-1,0), .33f, carryMask);
        if (rh)
            PickupObject(rh.transform.gameObject);
    }

    //Would be better if when we pickedup an object, we attached a  "carried" script to it, that way
    //reseting back to the proper values is easier
    private void PickupObject(GameObject toPickUp)
    {
        toPickUp.transform.SetParent(transform);
        toPickUp.transform.localPosition = localHandPos;
        carryingObject = toPickUp;
        Collider2D coli = toPickUp.GetComponent<Collider2D>();
        Rigidbody2D rb = toPickUp.GetComponent<Rigidbody2D>();
        if(coli) coli.enabled = false;
        if (rb) rb.bodyType = RigidbodyType2D.Kinematic;
        isCarrying = true;
        
    }

    IEnumerator ThrowCarriedObject(GameObject toThrow, Vector2 displacement, float throwTime)
    {
        Vector2 startPos = toThrow.transform.position;
        Vector2 endPos = startPos + displacement;
        float endTime = Time.time + throwTime;
        //See, more examples where turning off and on colliders everywhere! Should just be a "carried/thrown" script!
        Collider2D coli = toThrow.GetComponent<Collider2D>();
        if(coli) coli.enabled = false;
        while(Time.time < endTime)
        {
            float perctenage = 1 - ((endTime - Time.time) / throwTime);
            toThrow.transform.position = new Vector2(Mathf.Lerp(startPos.x,endPos.x,perctenage),GV.ws.thrownAnim.Evaluate(perctenage) + startPos.y);
            yield return null;
        }
        Potion p = toThrow.GetComponent<Potion>();
        if (p)
            p.PotionExplodes();
        else if (toThrow.GetComponent<Collider2D>())
            toThrow.GetComponent<Collider2D>().enabled = true;
    }

    

    private void MakePotionPressed()
    {
        //So many raycasts checking for a direction, could be made more generic by having one location for all checks
        RaycastHit2D rh = Physics2D.Raycast((Vector2)transform.position + localHandPos, new Vector2(-1,0), 1f, (1 << LayerMask.NameToLayer("Cauldron")));
        if(rh)
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
            droppedObj.transform.localPosition = droppedObj.transform.localPosition + new Vector3(GV.Player_Drop_Distance, 0, 0);
        }

    }

    //Would be better if there was an "attached" and "deattached" script on object, simpler to remove
    //and reset things back to original, here we assume the rigidbody should be dynamic... dangerous
    private GameObject DeattachCarryingItem()
    {
        GameObject toRet = carryingObject;
        Collider2D coli = toRet.GetComponent<Collider2D>();
        Rigidbody2D rb = toRet.GetComponent<Rigidbody2D>();
        if (coli) coli.enabled = true;
        if (rb) rb.bodyType = RigidbodyType2D.Dynamic;
        isCarrying = false;
        carryingObject.transform.SetParent(null); //This is bad, what if it belonged to a parent group?
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
