using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesListController : MonoBehaviour
{
    public List<Upgrades> upgradesList;
    public GameObject upgradePrefab;
    private void Start()
    {
        ListUpgrades();
    }

    void ListUpgrades()
    {
        foreach (Upgrades upgrades in upgradesList)
        {
            GameObject obj = Instantiate(upgradePrefab, this.transform);
            obj.transform.GetComponent<UpgradesListItemController>().upgrades = upgrades;
        }
    }
}

[Serializable] public class Upgrades
{
    public float id;
    public Sprite icon;
    public string upgradeTitle;
    public string upgradeText;
    public double price;
    public enum Features { addCashier, addCustomer, multiplyProfit, increaseSpeed }
    public Features feature;
    public float multiplyRate = 1;
    public float addRate = 0;
}
