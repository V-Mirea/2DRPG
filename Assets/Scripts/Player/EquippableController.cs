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

    private void InventoryController_ActiveSlotChanged(object sender, SlotStateChangeEventArgs e) {
        if (HandLocation.childCount > 0) {
            Destroy(HandLocation.GetChild(0).gameObject);
        }

        if (e.InventoryObject?.GetComponent<Equipabble>() != null)
            EquipItem(e.InventoryObject);
    }

    public void EquipItem(GameObject item) {
        handItem = GameObject.Instantiate(item, HandLocation.position, Quaternion.identity, HandLocation);
        handItem.SetActive(true);
    }
}
