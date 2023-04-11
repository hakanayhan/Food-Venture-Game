using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public static Modifiers Instance;
    public List<WorkstationUpgrades> workstationUpgrades;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public WorkstationUpgrades GetWorkstationUpgradesForOrderItem(OrderItem orderItem)
    {
        foreach (WorkstationUpgrades upgrades in workstationUpgrades)
        {
            if (upgrades.orderItem == orderItem)
            {
                return upgrades;
            }
        }
        return null;
    }

    public int GetUpgradeLevel(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.level;
    }

    public double GetUpgradeCost(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return (orderItem.itemBaseUpgradeCost * upgrades.upgradeCostMultiplier);
    }
    public double GetUnlockCost(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.unlockCost;
    }

    public double GetOrderItemCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.costMultiplier;
    }

    public void UpgradeLevel(OrderItem orderItem, bool increaseCosts = true)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.level++;
        if (increaseCosts)
        {
            float checkRank = (upgrades.level % 10f) / 10;
            if (checkRank == 0)
            {
                upgrades.costMultiplyRate = 2;
                upgrades.upgradeCostMultiplyRate -= 0.9;
            }
            IncreaseCostMultiplier(orderItem);
            IncreaseUpgradeCostMultiplier(orderItem);
        }
    }
    void IncreaseCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.costMultiplier = Math.Round(upgrades.costMultiplier * upgrades.costMultiplyRate, 2);
        upgrades.costMultiplyRate -= 0.05;
    }

    void IncreaseUpgradeCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.upgradeCostMultiplier = Math.Round(upgrades.upgradeCostMultiplier * upgrades.upgradeCostMultiplyRate, 2);
        upgrades.upgradeCostMultiplyRate += 0.1;
    }
}

[Serializable] public class WorkstationUpgrades
{
    public OrderItem orderItem;
    public int level;
    public float unlockCost;
    public double costMultiplier;
    public double upgradeCostMultiplier;
    public double costMultiplyRate = 2;
    public double upgradeCostMultiplyRate = 1.4;

}