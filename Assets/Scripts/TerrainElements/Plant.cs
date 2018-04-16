using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour {
    
    public Ingredient ingredient;
    public bool isMoving = false;

    public int id { get; private set; }
    public Vector3Int basePos { get; private set; }

    private Sprite sprite;
    private BoxCollider2D collider;

    private float timePassed = 0f;
    private float mutationTime = 0f;

    public void Start() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = ingredient.ToColor();
        renderer.sortingLayerName = "Plant";

        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("Plant");
        gameObject.tag = "Plant";

        mutationTime = Random.Range(GV.ws.flowerMutationRange.x, GV.ws.flowerMutationRange.y);
        basePos = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
	}

	public void UpdatePlant (float _dt) {
        timePassed += _dt;

        if (timePassed >= mutationTime) {
            Mutate();
            timePassed = 0f;
            mutationTime = Random.Range(GV.ws.flowerMutationRange.x, GV.ws.flowerMutationRange.y);
        }
    }

    public void SetSprite (Sprite _sprite, int _id) {
        sprite = _sprite;
        id = _id;
    }

    public void DropPlant(Vector3Int _newPos) {
        basePos = _newPos;
    }

    private void Mutate() {
        if (!isMoving) {
            Vector2 position = GV.GetRandomSpotNear(transform.position, 1.9f);
            Vector3Int target = new Vector3Int((int)position.x, (int)position.y, 0);

            if (target != new Vector3Int((int)transform.position.x, (int)transform.position.y, 0) 
                && target.x > 1 && target.y > 1 && target.x < GV.Map_Size_XY.x && target.y < GV.Map_Size_XY.y
               )
                PlantsManager.Instance.TryToSpread(target, this);
        }       
    }
}
