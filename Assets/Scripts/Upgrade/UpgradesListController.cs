using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesListController : MonoBehaviour
{
    public static UpgradesListController Instance;
    public List<Upgrade> upgradesList;
    public GameObject upgradePrefab;
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
        ListUpgrades();
    }

    void Update()
    {
        if (parentGameObject.transform.childCount != _childCount)
        {
            _childCount = parentGameObject.transform.childCount;
            Wallet.Instance.RefreshUI();
        }
    }
    
    void ListUpgrades()
    {
        foreach (Upgrade upgrade in upgradesList)
        {
            GameObject obj = Instantiate(upgradePrefab, parentGameObject.transform);
            obj.transform.GetComponent<UpgradesListItemController>().upgrade = upgrade;
        }
    }
}

[Serializable] public class Upgrade
{
    public Sprite icon;
    public string upgradeTitle;
    public double price;
    public enum Features { addCashier, addCustomer, multiplyProfit, increaseSpeed, addCook }
    public Features feature;
    public float multiplyRate = 1;
    public float addRate = 0;
    public OrderItem orderItem;
}
