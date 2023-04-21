using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWorkstationWindow : Window
{
    public static UpgradeWorkstationWindow Instance;
    public Wallet wallet;
    [SerializeField] GameObject panel;
    [SerializeField] WorkstationUpgrader upgrader;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] Image progressBarFill;
    [SerializeField] private Button buttonObj;
    [SerializeField] private GameObject coinObj;
    [SerializeField] private GameObject stars;
    [SerializeField] private GameObject starPrefab;
    public List<GameObject> starsList;
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
            SetStars(upgrader, rank);
        }
        else
        {
            float rank = Modifiers.Instance.GetRank(upgrader.orderItem);
            upgrader.upgradeCostLabel.text = "Max  ";
            progressBar.SetFillAmount(1);
            buttonObj.interactable = false;
            coinObj.SetActive(false);
            SetStars(upgrader, rank);
        }
    }

    void SetStars(WorkstationUpgrader upgrader, float rank)
    {
        int ranksCount = Modifiers.Instance.GetRanksCount(upgrader.orderItem);
        ResetStars();

        string colorHex = "#42BDFF";
        if (rank > 6 && ranksCount > 6)
            colorHex = "#88DD4A";

        progressBarFill.color = HexToColor(colorHex);
        int fixedRanksCount = (ranksCount > 6) ? ranksCount - 5 : ranksCount;
        fixedRanksCount = (ranksCount > 6 && rank <= 6) ? 6 : fixedRanksCount;
        
        for (int i = 0; i < (fixedRanksCount - 1); i++) {
            GameObject obj = Instantiate(starPrefab, stars.transform);
            starsList.Add(obj);
        }

        starsList.ForEach(star => star.SetActive(true));

        float fixedRank = (rank > 6 && ranksCount > 6) ? rank - 5 : rank;
        for (int i = 0; i < fixedRank - 1; i++)
        {
            starsList[i].GetComponent<Image>().color = HexToColor(colorHex);
        }
    }

    void ResetStars()
    {
        starsList.Clear();
        foreach (Transform star in stars.transform)
        {
            Destroy(star.gameObject);
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

    private Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
