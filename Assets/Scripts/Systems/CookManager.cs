using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;
    public bool hasCooks;
    public List<Order> pendingOrders = new List<Order>();
    public List<WorkStation> workStations;
    public CashierManager cashierManager;

    void Awake()
    {
        Instance = this;
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
                                WorkStation availableWorkstation = FindAvailableWorkStation(pendingOrder.orderItem);
                                if (availableWorkstation == null)
                                    continue;

                                cashier.isIdle = false;
                                cashier.cook.AssignWorkstation(availableWorkstation);
                                cashier.cook.CookOrder(pendingOrder);
                                Debug.Log("pending orders" + pendingOrders.Count);
                                pendingOrders.Remove(pendingOrder);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }


    public WorkStation FindAvailableWorkStation(OrderItem orderItem)
    {
        Debug.Log("Find Available Workstation out of " + workStations.Count);
        foreach (WorkStation workStation in workStations)
        {
            if (!workStation.gameObject.activeInHierarchy)
            {
                 Debug.Log(workStation.name + " is not active");
                continue;
            }
            if (!workStation.isBusy && workStation.orderItem == orderItem)
            {
                workStation.isBusy = true;
                return workStation;
            }
        }

        //Debug.LogError("Workstation could not be found");
        return null;
    }

    private void SpawnCook(){ } // add cook spawner
}
