using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public InventoryController InventoryController;

    private GameObject[] inventorySlots;
    private int activeSlot;

    void Awake() {
        /*
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.root.gameObject);
        
        inputActions = new InputMaster();
        inputActions.Enable();
        inputActions.UI.OpenInventory.performed += ctx => ToggleInventory();
        */

        // Probably want to create the inventory slots here

        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        activeSlot = InventoryController.ActiveSlot;
        HighlightSlot(activeSlot);
    
        InventoryController.ItemPickedUp += InventoryController_ItemPickedUp;
        InventoryController.ActiveSlotChanged += InventoryController_ActiveSlotChanged;
    }

    private void InventoryController_ActiveSlotChanged(object sender, int slot) {
        UnhighlightSlot(activeSlot);
        HighlightSlot(slot);
        activeSlot = slot;
    }

    private void InventoryController_ItemPickedUp(object sender, SlotStateChangeEventArgs e) {
        AddItem(e.InventoryRepresentation, e.SlotIndex);
    }

    public GameObject AddItem(GameObject InventoryItem, int index) {
        //if (!itemType.IsSubclassOf(typeof(InventoryItem))) {
            //throw new ArgumentException($"itemType (currently {itemType.Name}) should derive from InventoryItem");
        //}

        
        GameObject inventorySlot = inventorySlots[index];
        var inventoryItem = Instantiate(InventoryItem, inventorySlot.transform.position, Quaternion.identity, inventorySlot.transform);
        inventoryItem.transform.localScale = Vector3.one;

        return inventoryItem;
    }

    public void ToggleInventory() {
        gameObject.GetComponent<CanvasGroup>().alpha = gameObject.GetComponent<CanvasGroup>().alpha == 0f ? 1f : 0f;
    } 

    private void HighlightSlot(int slot) {
        inventorySlots[slot].GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    private void UnhighlightSlot(int slot) {
        inventorySlots[slot].GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }
}
