using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;
    public bool hasCooks;
    public List<Order> pendingOrders = new List<Order>();
    public List<Workstation> workstations;
    public CashierManager cashierManager;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cashierManager = FindObjectOfType<CashierManager>();
    }

    private void Update()
    {
        if(hasCooks)
            SpawnCook();

        ManageOrders();
    }
    public void ReceiveOrder(Order order)
    {
        pendingOrders.Add(order);
    }
    
    public void ManageOrders()
    {
        if (pendingOrders.Count > 0)
        {
            if (!hasCooks) // if cashiers are used instead of cooks
            {
                List<CashierStateMachine> availableCashiers = new List<CashierStateMachine>();
                foreach (CashierStateMachine cashier in cashierManager.cashiers)
                {
                    if (cashier.isIdle)
                        availableCashiers.Add(cashier);
                }
                if (availableCashiers.Count != 0 && cashierManager.areOrdersTaken())
                {
                    foreach (Order pendingOrder in pendingOrders)
                    {
                        Workstation availableWorkstation = FindAvailableWorkstation(pendingOrder.orderItem);
                        if (availableWorkstation == null)
                            continue;

                        CashierStateMachine closestCashier = CashierManager.Instance.GetClosestCashier(availableCashiers, availableWorkstation.CookTransform);
                        closestCashier.isIdle = false;
                        closestCashier.cook.AssignWorkstation(availableWorkstation);
                        closestCashier.cook.CookOrder(pendingOrder);
                        pendingOrders.Remove(pendingOrder);
                        return;
                    }
                }
            }
        }
    }


    public Workstation FindAvailableWorkstation(OrderItem orderItem)
    {
        //Debug.Log("Find Available Workstation out of " + workstations.Count);
        foreach (Workstation workstation in workstations)
        {
            if (!workstation.gameObject.activeInHierarchy)
            {
                 //Debug.Log(workstation.name + " is not active");
                continue;
            }
            if (!workstation.isBusy && workstation.orderItem == orderItem)
            {
                workstation.isBusy = true;
                return workstation;
            }
        }

        //Debug.LogError("Workstation could not be found");
        return null;
    }

    private void SpawnCook(){ } // add cook spawner
}
