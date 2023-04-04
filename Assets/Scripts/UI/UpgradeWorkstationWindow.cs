using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeWorkstationWindow : MonoBehaviour
{
    public static UpgradeWorkstationWindow Instance;
    public Wallet wallet;
    [SerializeField] GameObject panel;
    [SerializeField] WorkstationUpgrader upgrader;
    bool isOpen;
    private double upgradeCost;
    private double itemCost;
    private float itemLevel;
    private string itemName;

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
        wallet = FindObjectOfType<Wallet>();
    }

    void LoadDataForWorkstationUpgrader(WorkstationUpgrader upgrader)
    {
        itemLevel = upgrader.orderItem.itemLevel;
        itemCost = upgrader.orderItem.itemCost;
        itemName = upgrader.orderItem.itemName;
        upgradeCost = itemCost * 5;
        upgrader.levelLabel.text = "Level " + itemLevel;
        upgrader.itemNameLabel.text = itemName;
        upgrader.upgradeCostLabel.text = upgradeCost.ToString();
        upgrader.itemCostLabel.text = itemCost.ToString();
    }

    public void OpenWindow(WorkstationUpgrader upgrader)
    {
        this.upgrader = upgrader;
        LoadDataForWorkstationUpgrader(upgrader);
        if (isOpen)
        {
            isOpen = false;
            panel.SetActive(false);
        }
        else
        {
            isOpen = true;
            panel.SetActive(true);
        }
    }

    public void LvUpButton()
    {
        if(wallet.goldAmount >= upgradeCost)
        {
            wallet.goldAmount -= upgradeCost;
            upgrader.orderItem.itemLevel++;
            upgrader.orderItem.itemCost += 5;
            LoadDataForWorkstationUpgrader(upgrader);
        }
        else
        {
            Debug.Log("not enough gold");
        }
    }
}
