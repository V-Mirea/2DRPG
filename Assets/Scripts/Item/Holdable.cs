using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            var inventoryController = other.GetComponent<InventoryController>();
            inventoryController.TryPickupItem(gameObject);
        }
    }
}
