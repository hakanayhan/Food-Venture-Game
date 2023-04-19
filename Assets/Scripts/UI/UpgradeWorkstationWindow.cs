using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWorkstationWindow : Window
{
    public static UpgradeWorkstationWindow Instance;
    public Wallet wallet;
    [SerializeField] GameObject panel;
    [SerializeField] WorkstationUpgrader upgrader;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] private Button buttonObj;
    [SerializeField] private GameObject coinObj;
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

        upgrader.levelLabel.text = "Level " + itemLevel;
        upgrader.itemNameLabel.text = itemName;
        upgrader.upgradeCostLabel.text = upgradeCost.ToString();
        upgrader.itemCostLabel.text = itemCost.ToString();
        upgrader.itemCookTimeLabel.text = itemCookTime.ToString() + "s";

        SetRank(upgrader);
    }

    void SetRank(WorkstationUpgrader upgrader)
    {
        if (!Modifiers.Instance.GetIsMaxLv(upgrader.orderItem))
        {
            float rank = Modifiers.Instance.GetRank(upgrader.orderItem);
            float prevRankLv = Modifiers.Instance.GetRankUpLevel(upgrader.orderItem, (int)rank - 1);
            float rankLv = Modifiers.Instance.GetRankUpLevel(upgrader.orderItem, (int)rank);
            float progress = ((itemLevel - prevRankLv) % (rankLv - prevRankLv)) / (rankLv - prevRankLv);
            progressBar.SetFillAmount(progress);
            buttonObj.interactable = true;
            coinObj.SetActive(true);
        }
        else
        {
            upgrader.upgradeCostLabel.text = "Max    ";
            progressBar.SetFillAmount(1);
            buttonObj.interactable = false;
            coinObj.SetActive(false);
        }
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
