using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool initialized = false;
    InputManager inputManager;
    Rigidbody2D rb;

    //Movement
    readonly float PLAYER_MOVE_VELO = 3;

    //Carry mechanics
    bool isCarrying = false;
    readonly float carryingRange = .7f;
    Vector2 localHandPos;

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

    }

    public void UpdatePlayer(float dt)
    {
        inputManager.UpdateManager(dt);
        InputManager.InputInfo inputInfo = inputManager.GetInputInfo();
        if(inputInfo.pickDropPressed)
            if (isCarrying)
                DroppedPresed();
            else
                PickupPressed();
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = dir.normalized * PLAYER_MOVE_VELO;
    }

    private void PickupPressed()
    {
        RaycastHit2D rh = Physics2D.Raycast((Vector2)transform.position, new Vector2(), 1f, 1 << LayerMask.NameToLayer("Goop"));
        if (rh)
            PickupObject(rh.transform.gameObject);
    }

    private void PickupObject(GameObject toPickUp)
    {
        toPickUp.transform.SetParent(transform);
        toPickUp.transform.localPosition = localHandPos;
    }

    private void DroppedPresed()
    {
        Debug.Log("Dropped Pressed");
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
