using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RenovationWindow : MonoBehaviour
{
    public static RenovationWindow Instance;
    [SerializeField] GameObject window;
    [SerializeField] Camera cameraGameObject;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] Button renovateButton;
    [SerializeField] GameObject renovationUpgradeIcon;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void OpenRenovationWindow()
    {
        CheckRenovation();
        window.SetActive(true);
        cameraGameObject.GetComponent<CameraController>().enabled = false;
    }

    public void CloseRenovationWindow()
    {
        window.SetActive(false);
        cameraGameObject.GetComponent<CameraController>().enabled = true;
    }

    public void RenovateButton()
    {
        if (Modifiers.Instance.AreAllFoodMaxLv() && LevelManager.Instance.IsNextLvExist() && CanAffordRenovation())
            LevelManager.Instance.UpgradeLevel();
    }

    void CheckRenovation()
    {
        if (Modifiers.Instance.AreAllFoodMaxLv())
        {
            if (LevelManager.Instance.IsNextLvExist())
            {
                if(CanAffordRenovation())
                {
                    upgradeText.text = "You can upgrade your restaurant!";
                    renovateButton.interactable = true;
                }
                else
                {
                    Currency requiredGold = new Currency(LevelManager.Instance.requiredGoldsToLevelUp);
                    upgradeText.text = "You need " + requiredGold.ToString() + " golds to upgrade your restaurant!";
                    renovateButton.interactable = false;
                }
            }
            else
            {
                upgradeText.text = "It's the max level. Stay updated!";
                renovateButton.interactable = false;
            }
        }
        else
        {
            int maxLv = Modifiers.Instance.workstationUpgrades[0].upgradeRanks[Modifiers.Instance.workstationUpgrades[0].upgradeRanks.Count - 1].rankUpLevel;
            upgradeText.text = "Upgrade all stations to level " + maxLv + " first!";
            renovateButton.interactable = false;
        }
    }
    public void CheckRenovationIcon()
    {
        renovationUpgradeIcon.SetActive(Modifiers.Instance.AreAllFoodMaxLv() && CanAffordRenovation());
        if (window.activeSelf)
            CheckRenovation();
    }
    bool CanAffordRenovation()
    {
        return (Wallet.Instance.GetGoldBalance() >= LevelManager.Instance.requiredGoldsToLevelUp);
    }
}
