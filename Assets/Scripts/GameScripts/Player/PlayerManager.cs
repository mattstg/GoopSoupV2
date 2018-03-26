using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    #region Singleton
    private static PlayerManager instance;

    private PlayerManager() { }

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }
            return instance;
        }
    }
    #endregion

    public Player player { private set; get; }

    public void Initialize()
    {
        SpawnPlayerAtLoc(new Vector2());
    }

    public void Update(float dt)
    {
        if (player)
            player.UpdatePlayer(dt);
    }

    public void FixedUpdate(float dt)
    {
        if (player)
            player.FixedUpdatePlayer(dt);
    }

    public void PlayerDied()
    {

    }

    public void SpawnPlayerAtLoc(Vector2 atLoc)
    {
        if (!player)
        {
            GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            player = playerObj.GetComponent<Player>();
        }
        player.transform.position = atLoc;
        player.PlayerSpawned();
    }
}
