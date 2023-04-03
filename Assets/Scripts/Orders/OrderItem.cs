using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Order Item", order = 1)]
public class OrderItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public double itemCost;
    public float itemBaseCookTime;
    public float itemLevel;
    public float GetCookTime()
    {
        return itemBaseCookTime;
    }
}
