using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesListItemController : MonoBehaviour
{
    public Upgrade upgrade;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _description;
    [SerializeField] private Text _priceText;

    private void Start()
    {
        _icon.sprite = upgrade.icon;
        _title.text = upgrade.upgradeTitle;
        _description.text = GetDescriptionText();
        _priceText.text = new Currency(upgrade.price).ToString();
    }

    private string GetDescriptionText()
    {
        if (upgrade.feature == Upgrade.Features.addCashier)
        {
            string s = (upgrade.addRate > 1) ? "s" : "";
            return "+" + upgrade.addRate + " Cashier" + s;
        }
        else if (upgrade.feature == Upgrade.Features.addCustomer)
        {
            string s = (upgrade.addRate > 1) ? "s" : "";
            return "+" + upgrade.addRate + " Customer" + s;
        }
        else if (upgrade.feature == Upgrade.Features.multiplyProfit)
        {
            return upgrade.orderItem.itemName + " profit x" + upgrade.multiplyRate;
        }
        else if (upgrade.feature == Upgrade.Features.increaseSpeed)
        {
            return upgrade.orderItem.itemName + " is made faster";
        }
        return null;
    }

    public void UpgradeButton()
    {
        if (Wallet.Instance.TryRemoveGold(upgrade.price))
        {
            if(upgrade.feature == Upgrade.Features.addCashier)
            {
                CashierManager.Instance.maxCashier += upgrade.addRate;
            }
            else if(upgrade.feature == Upgrade.Features.addCustomer)
            {
                CustomerManager.Instance.maxCustomer += upgrade.addRate;
            }
            else if (upgrade.feature == Upgrade.Features.multiplyProfit)
            {
                Modifiers.Instance.MultiplyCostMultiplier(upgrade.orderItem, upgrade.multiplyRate);
            }
            else if (upgrade.feature == Upgrade.Features.increaseSpeed)
            {
                Modifiers.Instance.MultiplyCookTimeRatio(upgrade.orderItem, upgrade.multiplyRate);
            }
            Destroy(this.gameObject);
        }
    }
}
