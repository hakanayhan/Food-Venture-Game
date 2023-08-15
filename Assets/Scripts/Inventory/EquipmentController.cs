using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    public InventoryObject inventory;
    public ItemType type;
    public ItemObject item;
    public Image rarity;
    public Image icon;
    public GameObject levelObject;
    [SerializeField] private Sprite defaultIcon;

    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        if (type == ItemType.Hat)
        {
            item = inventory.equippedHat;
        }
        else if (type == ItemType.Apron)
        {
            item = inventory.equippedApron;
        }
        else if (type == ItemType.Spatula)
        {
            item = inventory.equippedSpatula;
        }

        if(item == null)
        {
            icon.sprite = defaultIcon;
            icon.color = Utility.HexToColor("D0D0D0");
            rarity.color = Utility.HexToColor("D0D0D0");
            levelObject.SetActive(false);
        }
        else
        {
            icon.sprite = item.icon;
            icon.color = Color.white;
            rarity.color = InventoryListController.Instance.GetRarityColor(item.rarity);
            levelObject.SetActive(true);
        }
    }
}
