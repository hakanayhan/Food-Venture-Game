using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public static Modifiers Instance;
    public List<WorkstationUpgrades> workstationUpgrades;
    [HideInInspector]public CookManager cookManager;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        cookManager = FindObjectOfType<CookManager>();
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

    public Currency GetUpgradeCost(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return new Currency(orderItem.itemBaseUpgradeCost * upgrades.upgradeCostMultiplier);
    }
    public Currency GetUnlockCost(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return new Currency(upgrades.unlockCost);
    }

    public double GetOrderItemCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.costMultiplier;
    }

    public float GetCookTimeRatio(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.cookTimeRatio;
    }

    public void UpgradeLevel(WorkstationUpgrader upgrader, bool increaseCosts = true)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(upgrader.orderItem);
        upgrades.level++;
        if (increaseCosts)
        {
            float checkRank = (upgrades.level % 10f) / 10;
            if (checkRank == 0)
            {
                if (upgrades.activeWorkstations < upgrader.stationGameObjects.Count)
                {
                    upgrader.stationGameObjects[upgrades.activeWorkstations].SetActive(true);
                    cookManager.workstations.Add(upgrader.stationGameObjects[upgrades.activeWorkstations].GetComponent<Workstation>());
                    upgrades.activeWorkstations++;
                }
                upgrades.costMultiplier *= 2;
            }
            IncreaseCostMultiplier(upgrader.orderItem);
            IncreaseUpgradeCostMultiplier(upgrader.orderItem);
        }
    }
    void IncreaseCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.costMultiplier = Math.Round(upgrades.costMultiplier * upgrades.costMultiplyRate, 2);
        if (upgrades.costMultiplyRate > upgrades.minCostMultiplyRate)
        {
            upgrades.costMultiplyRate -= (upgrades.costMultiplyRate / 10);
            if(upgrades.costMultiplyRate < upgrades.minCostMultiplyRate)
                upgrades.costMultiplyRate = upgrades.minCostMultiplyRate;
        }
    }

    void IncreaseUpgradeCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.upgradeCostMultiplier = Math.Round(upgrades.upgradeCostMultiplier * upgrades.upgradeCostMultiplyRate, 2);
        if (upgrades.upgradeCostMultiplyRate > upgrades.minUpgradeCostMultiplyRate)
        {
            upgrades.upgradeCostMultiplyRate -= (upgrades.upgradeCostMultiplyRate / 40);
            if (upgrades.upgradeCostMultiplyRate < upgrades.minUpgradeCostMultiplyRate)
                upgrades.upgradeCostMultiplyRate = upgrades.minUpgradeCostMultiplyRate;
        }
    }

    public void MultiplyCostMultiplier(OrderItem orderItem, float multiplyRate)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.costMultiplier *= multiplyRate;
    }

    public void MultiplyCookTimeRatio(OrderItem orderItem, float multiplyRate)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.cookTimeRatio *= multiplyRate;
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
    public double minCostMultiplyRate = 1.25;
    public double upgradeCostMultiplyRate = 1.4;
    public double minUpgradeCostMultiplyRate = 1.4;
    public float cookTimeRatio = 1;
    public int activeWorkstations = 1;
}