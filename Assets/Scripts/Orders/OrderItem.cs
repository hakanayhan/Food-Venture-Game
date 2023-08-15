using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Order Item", order = 1)]
public class OrderItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public Material itemMaterial;
    public double itemBaseCost;
    public double itemBaseUpgradeCost;
    public float itemBaseCookTime;
    public float GetCookTime()
    {
        return itemBaseCookTime / Modifiers.Instance.GetCookTimeRatio(this);
    }

    public Currency GetCost()
    {
        return new Currency(itemBaseCost * Modifiers.Instance.GetOrderItemCostMultiplier(this) * EquippedItemsController.Instance.profitBoost);
    }
}
