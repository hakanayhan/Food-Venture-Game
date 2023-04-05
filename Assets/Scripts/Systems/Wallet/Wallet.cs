using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public UIManager UIManager;
    public double goldAmount;

    private void Start()
    {
        UIManager.SetGoldText(goldAmount);
    }
    public void AddGold(double gold)
    {
        goldAmount += gold;
        UIManager.SetGoldText(goldAmount);
    }

    public void RemoveGold(double gold)
    {
        goldAmount -= gold;
        UIManager.SetGoldText(goldAmount);
    }
}
