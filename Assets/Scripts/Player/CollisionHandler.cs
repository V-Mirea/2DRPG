using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public  InventoryController inventoryController;

    private void OnTriggerEnter2D(Collider2D other) {
        var entityComponent = other.gameObject.GetComponent<Entity>();
        if (entityComponent?.data.CanBePickedUp ?? false) {
            inventoryController.PickupItem(other.gameObject);
        }
    }
}
