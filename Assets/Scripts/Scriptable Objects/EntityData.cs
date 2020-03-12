using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "Entity Data", order = 51)]
public class EntityData : ScriptableObject
{
    [SerializeField]
    public GameObject InventoryRepresentation;

    public bool CanBePickedUp => InventoryRepresentation != null;
}
