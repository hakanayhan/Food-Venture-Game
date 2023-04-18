using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesListItemController : MonoBehaviour
{
    public Upgrades upgrades;

    private void Start()
    {
        this.transform.GetChild(0).GetComponent<Image>().sprite = upgrades.icon;
        this.transform.GetChild(1).GetComponent<Text>().text = upgrades.upgradeTitle;
        this.transform.GetChild(2).GetComponent<Text>().text = upgrades.upgradeText;
        this.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = upgrades.price.ToString();
    }
}
