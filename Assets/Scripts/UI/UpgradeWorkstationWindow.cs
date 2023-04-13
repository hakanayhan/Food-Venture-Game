using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWorkstationWindow : MonoBehaviour
{
    public static UpgradeWorkstationWindow Instance;
    public Wallet wallet;
    [SerializeField] GameObject panel;
    [SerializeField] WorkstationUpgrader upgrader;
    [SerializeField] ProgressBar progressBar;
    bool isOpen;
    private Currency upgradeCost;
    private Currency itemCost;
    private int itemLevel;
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
        itemLevel = Modifiers.Instance.GetUpgradeLevel(upgrader.orderItem);
        itemName = upgrader.orderItem.itemName;
        itemCost = upgrader.orderItem.GetCost();
        upgradeCost = Modifiers.Instance.GetUpgradeCost(upgrader.orderItem);
        float progress = (itemLevel % 10f) / 10;
        progressBar.SetFillAmount(progress);
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
        if (Wallet.Instance.TryRemoveGold(upgradeCost))
        {
            Modifiers.Instance.UpgradeLevel(upgrader);
            LoadDataForWorkstationUpgrader(upgrader);
        }
    }
}
