using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Order Item", order = 1)]
public class OrderItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public double itemBaseCost;
    public double itemBaseUpgradeCost;
    public float itemBaseCookTime;
    public float GetCookTime()
    {
        return itemBaseCookTime;
    }

    public double GetCost()
    {
        double itemCost = itemBaseCost * Modifiers.Instance.GetOrderItemCostMultiplier(this);
        return itemCost;
    }
}
