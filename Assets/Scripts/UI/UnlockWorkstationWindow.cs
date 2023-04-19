using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorkstationWindow : Window
{
    public static UnlockWorkstationWindow Instance;
    [SerializeField] WorkstationUpgrader upgrader;
    [SerializeField] GameObject panel;
    CustomerManager customerManager;
    public Wallet wallet;
    Currency unlockCost;
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
        
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }

    public override void CloseWindow()
    {
        panel.SetActive(false);
    }

    public void UnlockButton()
    {
        if (Wallet.Instance.TryRemoveGold(unlockCost))
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        customerManager.AddItem(upgrader.orderItem);
        Modifiers.Instance.UpgradeLevel(upgrader, false);
        upgrader.unlockGameObject.SetActive(false);
        upgrader.baseStationGameObject.SetActive(true);
        upgrader.stationGameObjects[0].SetActive(true);
        CloseWindowsOnClick.Instance.CloseAllWindows();
    }
}
