using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesListController : MonoBehaviour
{
    public List<Upgrade> upgradesList;
    public GameObject upgradePrefab;
    private void Start()
    {
        ListUpgrades();
    }

    void ListUpgrades()
    {
        foreach (Upgrade upgrade in upgradesList)
        {
            GameObject obj = Instantiate(upgradePrefab, this.transform);
            obj.transform.GetComponent<UpgradesListItemController>().upgrade = upgrade;
        }
    }
}

[Serializable] public class Upgrade
{
    public Sprite icon;
    public string upgradeTitle;
    public double price;
    public enum Features { addCashier, addCustomer, multiplyProfit, increaseSpeed }
    public Features feature;
    public float multiplyRate = 1;
    public float addRate = 0;
    public OrderItem orderItem;
}
