using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesListController : MonoBehaviour
{
    public List<Upgrades> upgradesList;
    public GameObject upgradePrefab;
    private void Start()
    {
        listUpgrades();
    }

    void listUpgrades()
    {
        foreach (Upgrades upgrades in upgradesList)
        {
            GameObject obj = Instantiate(upgradePrefab, this.transform);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = upgrades.icon;
            obj.transform.GetChild(1).GetComponent<Text>().text = upgrades.upgradeTitle;
            obj.transform.GetChild(2).GetComponent<Text>().text = upgrades.upgradeText;
            obj.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = upgrades.price.ToString();
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
