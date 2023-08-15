using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Hat,
    Apron,
    Spatula
}

public enum Rarity
{
    Common
}

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/New Inventory Item")]
public class ItemObject : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public ItemType type;
    public Rarity rarity;
}
