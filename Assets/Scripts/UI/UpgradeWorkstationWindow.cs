using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWorkstationWindow : Window
{
    public static UpgradeWorkstationWindow Instance;
    public Wallet wallet;
    [SerializeField] GameObject panel;
    [SerializeField] WorkstationUpgrader upgrader;
    [SerializeField] ProgressBar progressBar;
    private Currency upgradeCost;
    private Currency itemCost;
    private int itemLevel;
    private float itemCookTime;
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
        itemCookTime = upgrader.orderItem.GetCookTime();
        upgradeCost = Modifiers.Instance.GetUpgradeCost(upgrader.orderItem);
        float progress = (itemLevel % 10f) / 10;
        progressBar.SetFillAmount(progress);
        upgrader.levelLabel.text = "Level " + itemLevel;
        upgrader.itemNameLabel.text = itemName;
        upgrader.upgradeCostLabel.text = upgradeCost.ToString();
        upgrader.itemCostLabel.text = itemCost.ToString();
        upgrader.itemCookTimeLabel.text = itemCookTime.ToString() + "s";
    }

    public void OpenWindow(WorkstationUpgrader upgrader)
    {
        this.upgrader = upgrader;
        LoadDataForWorkstationUpgrader(upgrader);
        
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }

    public override void CloseWindow()
    {
        panel.SetActive(false);
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
