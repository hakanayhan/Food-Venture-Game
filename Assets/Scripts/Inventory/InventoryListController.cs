using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryListController : MonoBehaviour
{
    public static InventoryListController Instance;
    public InventoryObject inventory;
    public GameObject itemPrefab;
    public GameObject parentGameObject;
    private int _childCount = -1;

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
        ListItems();
    }

    void Update()
    {
        if (parentGameObject.transform.childCount != _childCount)
        {
            _childCount = parentGameObject.transform.childCount;
            Wallet.Instance.RefreshUI();
        }
    }

    void ListItems()
    {
        foreach (ItemObject item in inventory.inventoryList)
        {
            GameObject obj = Instantiate(itemPrefab, parentGameObject.transform);
            obj.transform.GetComponent<ItemController>().item = item;
        }
    }

    public Color GetRarityColor(Rarity rarity)
    {
        if (rarity == Rarity.Common)
        {
            return Utility.HexToColor("5F82F3");
        }
        else
        {
            return Utility.HexToColor("D0D0D0");
        }
    }
}
