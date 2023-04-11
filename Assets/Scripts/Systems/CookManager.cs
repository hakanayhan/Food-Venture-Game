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
                foreach (CashierStateMachine cashier in cashierManager.cashiers)
                {
                    if (cashier.isIdle)
                    {
                        if (cashierManager.areOrdersTaken()) {
                            
                            foreach (Order pendingOrder in pendingOrders)
                            {
                                Workstation availableWorkstation = FindAvailableWorkstation(pendingOrder.orderItem);
                                if (availableWorkstation == null)
                                    continue;

                                cashier.isIdle = false;
                                cashier.cook.AssignWorkstation(availableWorkstation);
                                cashier.cook.CookOrder(pendingOrder);
                                //Debug.Log("pending orders" + pendingOrders.Count);
                                pendingOrders.Remove(pendingOrder);
                                return;
                            }
                        }
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
