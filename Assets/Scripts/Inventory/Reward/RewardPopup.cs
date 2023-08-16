using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardPopup : MonoBehaviour
{
    public static RewardPopup Instance;
    public GameObject window;
    public Camera cameraGameObject;
    public InventoryObject inventory;

    public ItemObject item;
    public Image rarity;
    public Image icon;
    public TextMeshProUGUI featureText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OpenRewardPopup(ItemObject item)
    {
        this.item = item;
        inventory.inventoryList.Add(item);

        icon.sprite = item.icon;
        rarity.color = InventoryListController.Instance.GetRarityColor(item.rarity);
        if(item.featureType == FeatureType.IncreaseAllProfit)
            featureText.text = "+" + item.featureAmount + "% All Profit";

        cameraGameObject.GetComponent<CameraController>().enabled = false;
        window.SetActive(true);
    }

    public void EquipReward()
    {
        if (item.type == ItemType.Hat)
        {
            inventory.equippedHat = item;
        }
        else if (item.type == ItemType.Apron)
        {
            inventory.equippedApron = item;
        }
        else if (item.type == ItemType.Spatula)
        {
            inventory.equippedSpatula = item;
        }
        EquippedItemsController.Instance.AdjustBonuses();
        window.SetActive(false);
        cameraGameObject.GetComponent<CameraController>().enabled = true;
    }
    public void KeepReward()
    {
        window.SetActive(false);
        cameraGameObject.GetComponent<CameraController>().enabled = true;
    }
}
