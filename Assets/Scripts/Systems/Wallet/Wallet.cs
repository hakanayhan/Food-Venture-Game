using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;
    public UIManager UIManager;

    public double startingGoldAmount;
    private Currency _goldAmount;

    [HideInInspector]public Currency goldAmount
    {
        get { return _goldAmount; }
        set
        {
            _goldAmount = value;
            UIManager.SetGoldText(_goldAmount);
            RefreshUI();
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        goldAmount = new Currency(startingGoldAmount);
        UIManager = FindObjectOfType<UIManager>();
    }

    public void AddGold(double amtToAdd)
    {
        goldAmount += amtToAdd;
    }

    public bool TryRemoveGold(double gold)
    {
        if (goldAmount < gold)
            return false;

        goldAmount -= gold;
        return true;
    }

    public bool TryRemoveGold(Currency gold)
    {
        return TryRemoveGold((double)gold);
    }

    public Currency GetGoldBalance()
    {
        return goldAmount;
    }
    
    public void SetGold(double amt)
    {
        goldAmount = new Currency(amt);
    }

    public void RefreshUI()
    {
        foreach (WorkstationUpgrader upgrader in Modifiers.Instance.foods)
        {
            upgrader.AdjustUpgradeIcon();
        }


        UIManager.upgradeIcon.SetActive(false);
        foreach (UpgradesListItemController upgradeItem in UpgradesListController.Instance.parentGameObject.GetComponentsInChildren<UpgradesListItemController>())
        {
            if (goldAmount >= upgradeItem.upgrade.price)
            {
                UIManager.Instance.upgradeIcon.SetActive(true);
                upgradeItem.upgradeButton.interactable = true;
            }
            else
            {
                upgradeItem.upgradeButton.interactable = false;
            }
        }

        OrderItem orderItem = UpgradeWorkstationWindow.Instance.upgrader.orderItem;
        bool canAffordUpgrade = goldAmount >= Modifiers.Instance.GetUpgradeCost(orderItem);
        bool canAffordUnlock = goldAmount >= Modifiers.Instance.GetUnlockCost(orderItem);
        UpgradeWorkstationWindow.Instance.buttonObj.interactable = !Modifiers.Instance.GetIsMaxLv(orderItem) && canAffordUpgrade;
        UnlockWorkstationWindow.Instance.buttonObj.interactable = canAffordUnlock;
    }
}
