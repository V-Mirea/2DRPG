using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InventoryController : MonoBehaviour
{
    public int TotalSlots = 5;

    public int ActiveSlot { get; private set; }
    public int EmptySlots => items.Where(i => i is null).Count();

    private InventoryItemData[] items;
    private InputMaster inputActions;

    public event EventHandler<SlotStateChangeEventArgs> ItemPickedUp;
    public event EventHandler<int> ActiveSlotChanged;

    private void Awake() {
        items = new InventoryItemData[TotalSlots];
    }

    public bool PickupItem(GameObject groundItem) {
        if (EmptySlots <= 0 || groundItem == null) {
            return false;
        }

        Entity groundEntity = groundItem.GetComponent<Entity>();
        if (groundEntity == null) {
            throw new MissingComponentException("Cannot pickup a GameObject without an attached entity component");
        }

        int inventoryIndex = System.Array.IndexOf(items, null);
        items[inventoryIndex] = groundEntity.data.InventoryRepresentation.GetComponent<InventoryItem>().data;
        var eventArgs = new SlotStateChangeEventArgs() {
            InventoryRepresentation = groundEntity.data.InventoryRepresentation,
            SlotIndex = inventoryIndex
        };

        ItemPickedUp?.Invoke(this, eventArgs);
        Destroy(groundItem);
        return true;
    }

    public void ScrollItem(CallbackContext ctx) {
        if (!ctx.performed) return;

        short scrollDirection = (short)(ctx.ReadValue<float>() > 0 ? 1 : -1);

        ActiveSlot += scrollDirection;
        if (ActiveSlot >= TotalSlots) ActiveSlot = 0;
        else if (ActiveSlot < 0) ActiveSlot = TotalSlots - 1;

        /*ateChangeEventArgs eventArgs = new SlotStateChangeEventArgs() {
            InventoryRepresentation = items[ActiveSlot].,
        }
        */

        ActiveSlotChanged?.Invoke(this, ActiveSlot);
    }
}

public class SlotStateChangeEventArgs : EventArgs
{
    public GameObject InventoryRepresentation { get; set; }
    public int SlotIndex { get; set; }
}
