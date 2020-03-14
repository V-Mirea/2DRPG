using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    public bool MatchPlayer = false;

    private GameObject Player;
    private SpriteRenderer SpriteRenderer;
    private bool dynamicUpdate;

    void Awake() {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null && (rb.bodyType == RigidbodyType2D.Dynamic || rb.bodyType == RigidbodyType2D.Kinematic)) { 
            dynamicUpdate = true;
        } else {
            dynamicUpdate = false;
            SetLayerSortOrder();
        }
    }

    void Update() {
        if (dynamicUpdate || MatchPlayer) SetLayerSortOrder();
    }

    private void SetLayerSortOrder() {
        if (!MatchPlayer) {
            SpriteRenderer.sortingOrder = (int)(transform.position.y * -100);
        } else if (MatchPlayer) {
            SpriteRenderer.sortingOrder = Player.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}
