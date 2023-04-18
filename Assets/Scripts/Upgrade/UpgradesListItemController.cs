using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesListItemController : MonoBehaviour
{
    public Upgrades upgrades;
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Text priceText;

    private void Start()
    {
        icon.sprite = upgrades.icon;
        title.text = upgrades.upgradeTitle;
        description.text = upgrades.upgradeText;
        priceText.text = upgrades.price.ToString();
    }

    public void upgradeButton()
    {
        Debug.Log("Upgraded");
    }
}
