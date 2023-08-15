using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory/New Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<ItemObject> inventoryList = new List<ItemObject>();
}
