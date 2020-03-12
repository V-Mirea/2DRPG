using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private Transform player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) this.enabled = false;
    }

    void Update() {
        if (this.transform.position.y > player.position.y) SpriteRenderer.sortingOrder = -1;
        else SpriteRenderer.sortingOrder = 1;
    }
}
