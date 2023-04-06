using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorkstationWindow : MonoBehaviour
{
    public static UnlockWorkstationWindow Instance;
    [SerializeField] WorkstationUpgrader upgrader;
    [SerializeField] GameObject panel;
    CustomerManager customerManager;
    public Wallet wallet;
    double unlockCost;
    bool isOpen;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        customerManager = FindObjectOfType<CustomerManager>();
        wallet = FindObjectOfType<Wallet>();
    }

    public void OpenWindow(WorkstationUpgrader upgrader)
    {
        this.upgrader = upgrader;
        unlockCost = Modifiers.Instance.GetUnlockCost(upgrader.orderItem);
        upgrader.unlockUpgradeLabel.text = unlockCost.ToString();
        if (isOpen)
        {
            isOpen = false;
            panel.SetActive(false);
        }
        else
        {
            isOpen = true;
            panel.SetActive(true);
        }
    }

    public void UnlockButton()
    {
        if (wallet.goldAmount >= unlockCost)
        {
            wallet.RemoveGold(unlockCost);
            Unlock();
        }
    }

    public void Unlock()
    {
        customerManager.AddItem(upgrader.orderItem);
        Modifiers.Instance.UpgradeLevel(upgrader.orderItem, false);
        upgrader.unlockGameObject.SetActive(false);
        upgrader.station1GameObject.SetActive(true);
        isOpen = false;
        panel.SetActive(false);
    }
}
