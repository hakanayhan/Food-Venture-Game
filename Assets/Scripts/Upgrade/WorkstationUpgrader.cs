using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkstationUpgrader : MonoBehaviour
{
    public OrderItem orderItem;
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI itemNameLabel;
    public TextMeshProUGUI itemCostLabel;
    public TextMeshProUGUI upgradeCostLabel;
    public TextMeshProUGUI itemCookTimeLabel;
    public TextMeshProUGUI unlockUpgradeLabel;
    public List <GameObject> stationGameObjects;
    public GameObject unlockGameObject;
    public GameObject baseStationGameObject;
    public GameObject upgradeIcon;

    public void OnChildClicked(bool isItUnlocker)
    {
        if (isItUnlocker)
        {
            UnlockWorkstationWindow.Instance.OpenWindow(this);
        }
        else
        {
            UpgradeWorkstationWindow.Instance.OpenWindow(this);
        }
    }

    public void AdjustUpgradeIcon()
    {
        double cost = (double)Modifiers.Instance.GetUpgradeCost(orderItem);
        if (unlockGameObject.activeSelf)
            cost = (double)Modifiers.Instance.GetUnlockCost(orderItem);

        if (Modifiers.Instance.GetIsMaxLv(orderItem))
        {
            upgradeIcon.SetActive(false);
        }
        else
        {
            upgradeIcon.SetActive(Wallet.Instance.GetGoldBalance() >= cost);
        }
    }
}