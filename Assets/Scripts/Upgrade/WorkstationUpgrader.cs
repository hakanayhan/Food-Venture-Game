using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkstationUpgrader : MonoBehaviour
{
    public OrderItem orderItem;
    public Text levelLabel;
    public Text itemNameLabel;
    public Text itemCostLabel;
    public Text upgradeCostLabel;

    public void OnChildClicked()
    {
        UpgradeWorkstationWindow.Instance.OpenWindow(this);
    }
}