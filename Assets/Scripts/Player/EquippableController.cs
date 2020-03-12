using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableController : MonoBehaviour
{
    public Transform HandLocation;
    public InventoryController InventoryController;

    private GameObject handItem;

    private void Awake() {
        if(InventoryController != null)
            InventoryController.ActiveSlotChanged += InventoryController_ActiveSlotChanged;
    }

    public void EquipItem(GameObject item) {
        Destroy(handItem);
        handItem = GameObject.Instantiate(item, HandLocation.position, Quaternion.identity, transform);
    }

    private void InventoryController_ActiveSlotChanged(object sender, int e) {
        throw new System.NotImplementedException();
    }
}
