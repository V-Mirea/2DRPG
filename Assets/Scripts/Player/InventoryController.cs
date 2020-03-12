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

    private GameObject[] items;
    private InputMaster inputActions;

    public event EventHandler<SlotStateChangeEventArgs> ItemPickedUp;
    public event EventHandler<SlotStateChangeEventArgs> ActiveSlotChanged;

    private void Awake() {
        items = new GameObject[TotalSlots];
    }

    public bool TryPickupItem(GameObject groundItem) {
        if (EmptySlots <= 0 || groundItem == null) {
            return false;
        }

        int inventoryIndex = System.Array.IndexOf(items, null);
        items[inventoryIndex] = groundItem;
        var eventArgs = new SlotStateChangeEventArgs() {
            InventoryObject = groundItem,
            SlotIndex = inventoryIndex
        };

        ItemPickedUp?.Invoke(this, eventArgs);
        if (inventoryIndex == ActiveSlot) ActiveSlotChanged?.Invoke(this, eventArgs);

        groundItem.SetActive(false);
        return true;
    }

    public void ScrollItem(CallbackContext ctx) {
        if (!ctx.performed) return;

        short scrollDirection = (short)(ctx.ReadValue<float>() > 0 ? 1 : -1);

        ActiveSlot += scrollDirection;
        if (ActiveSlot >= TotalSlots) ActiveSlot = 0;
        else if (ActiveSlot < 0) ActiveSlot = TotalSlots - 1;

        SlotStateChangeEventArgs eventArgs = new SlotStateChangeEventArgs() {
            InventoryObject = items[ActiveSlot],
            SlotIndex = ActiveSlot
        };

        ActiveSlotChanged?.Invoke(this, eventArgs);
    }
}

public class SlotStateChangeEventArgs : EventArgs
{
    public GameObject InventoryObject { get; set; }
    public int SlotIndex { get; set; }
}
