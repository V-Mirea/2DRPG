using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryItem Data", menuName = "InventoryItem Data", order = 52)]
public class InventoryItemData : ScriptableObject
{
    [SerializeField]
    public GameObject EntityRepresentation;
}
