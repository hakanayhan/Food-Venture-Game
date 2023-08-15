using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsController : MonoBehaviour
{
    public static EquippedItemsController Instance;
    public InventoryObject inventory;

    public EquipmentController equippedHatController;
    public EquipmentController equippedApronController;
    public EquipmentController equippedSpatulaController;

    public ItemObject equippedHatItem;
    public ItemObject equippedApronItem;
    public ItemObject equippedSpatulaItem;

    public double profitBoost = 1;
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
        AdjustBonuses();
    }
    public void RefreshEquipments()
    {
        equippedHatController.Refresh();
        equippedApronController.Refresh();
        equippedSpatulaController.Refresh();
        AdjustBonuses();
    }
    public void AdjustBonuses()
    {
        AdjustEquippedItems();
        AdjustIncomeBonus();
    }
    public void AdjustEquippedItems()
    {
        equippedHatItem = inventory.equippedHat;
        equippedApronItem = inventory.equippedApron;
        equippedSpatulaItem = inventory.equippedSpatula;
    }
    public void AdjustIncomeBonus()
    {
        double totalIncomeBonus = 0;
        if (equippedHatItem != null && equippedHatItem.featureType == FeatureType.IncreaseAllProfit)
            totalIncomeBonus += equippedHatItem.featureAmount;
        if (equippedApronItem != null && equippedApronItem.featureType == FeatureType.IncreaseAllProfit)
            totalIncomeBonus += equippedApronItem.featureAmount;
        if (equippedSpatulaItem != null && equippedSpatulaItem.featureType == FeatureType.IncreaseAllProfit)
            totalIncomeBonus += equippedSpatulaItem.featureAmount;

        profitBoost = 1 + (totalIncomeBonus/100);
    }
}
