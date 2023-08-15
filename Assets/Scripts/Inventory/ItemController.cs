using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public ItemObject item;
    public InventoryObject inventory;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _rarityImage;

    private void Start()
    {
        _icon.sprite = item.icon;
        _rarityImage.color = InventoryListController.Instance.GetRarityColor(item.rarity);
    }

    public void Equip()
    {
        if (item.type == ItemType.Hat)
        {
            if(inventory.equippedHat == item)
            {
                inventory.equippedHat = null;
            }
            else
            {
                inventory.equippedHat = item;
            }
        }
        else if (item.type == ItemType.Apron)
        {
            if (inventory.equippedApron == item)
            {
                inventory.equippedApron = null;
            }
            else
            {
                inventory.equippedApron = item;
            }
        }
        else if (item.type == ItemType.Spatula)
        {
            if (inventory.equippedSpatula == item)
            {
                inventory.equippedSpatula = null;
            }
            else
            {
                inventory.equippedSpatula = item;
            }
        }
        EquippedItemsController.Instance.RefreshEquipments();
    }
}
