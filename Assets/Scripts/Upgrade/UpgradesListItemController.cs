using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesListItemController : MonoBehaviour
{
    public Upgrades upgrades;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _description;
    [SerializeField] private Text _priceText;

    private void Start()
    {
        _icon.sprite = upgrades.icon;
        _title.text = upgrades.upgradeTitle;
        _description.text = GetDescriptionText();
        _priceText.text = new Currency(upgrades.price).ToShortString();
    }

    private string GetDescriptionText()
    {
        if (upgrades.feature == Upgrades.Features.addCashier)
        {
            string s = (upgrades.addRate > 1) ? "s" : "";
            return "+" + upgrades.addRate + " Cashier" + s;
        }
        else if (upgrades.feature == Upgrades.Features.addCustomer)
        {
            string s = (upgrades.addRate > 1) ? "s" : "";
            return "+" + upgrades.addRate + " Customer" + s;
        }
        else if (upgrades.feature == Upgrades.Features.multiplyProfit)
        {
            return upgrades.orderItem.itemName + " profit x" + upgrades.multiplyRate;
        }
            return null;
    }

    public void UpgradeButton()
    {
        if (Wallet.Instance.TryRemoveGold(upgrades.price))
        {
            if(upgrades.feature == Upgrades.Features.addCashier)
            {
                CashierManager.Instance.maxCashier += upgrades.addRate;
            }
            else if(upgrades.feature == Upgrades.Features.addCustomer)
            {
                CustomerManager.Instance.maxCustomer += upgrades.addRate;
            }
            else if (upgrades.feature == Upgrades.Features.multiplyProfit)
            {
                Modifiers.Instance.MultiplyCostMultiplier(upgrades.orderItem, upgrades.multiplyRate);
            }
            Destroy(this.gameObject);
        }
    }
}
