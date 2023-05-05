using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;
    public UIManager UIManager;

    public double startingGoldAmount;
    [HideInInspector]public Currency goldAmount;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        goldAmount = new Currency(startingGoldAmount);
        UIManager = FindObjectOfType<UIManager>();
        UIManager.SetGoldText(goldAmount);
    }

    public void AddGold(double amtToAdd)
    {
        goldAmount += amtToAdd;
        UIManager.SetGoldText(goldAmount);
    }

    public bool TryRemoveGold(double gold)
    {
        if (goldAmount < gold)
            return false;

        goldAmount -= gold;
        UIManager.SetGoldText(goldAmount);
        return true;
    }

    public bool TryRemoveGold(Currency gold)
    {
        return TryRemoveGold((double)gold);
    }

    public Currency GetGoldBalance()
    {
        return goldAmount;
    }
    
    public void SetGold(double amt)
    {
        goldAmount = new Currency(amt);
        UIManager.SetGoldText(goldAmount);
    }
}
