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
    public Text itemCookTimeLabel;
    public Text unlockUpgradeLabel;
    public List <GameObject> stationGameObjects;
    public GameObject unlockGameObject;
    public GameObject baseStationGameObject;

    public void OnChildClicked(bool isItUnlocker)
    {
        if (isItUnlocker)
        {
            UnlockWorkstationWindow.Instance.OpenWindow(this);
        }
        else
        {
            UpgradeWorkstationWindow.Instance.OpenWindow(this);
        }
    }
}