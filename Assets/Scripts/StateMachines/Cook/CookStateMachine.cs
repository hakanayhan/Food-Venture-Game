using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookStateMachine : StateMachine
{
    public Order order;
    [HideInInspector] public Workstation workstation;
    [HideInInspector] public ChefStation chefStation;
    [HideInInspector] public CookManager cookManager;
    public RadialTimer radialTimer;
    public GameObject carriedItem;
    public CashierStateMachine cashierStateMachine;

    private void Start()
    {
        cookManager = FindObjectOfType<CookManager>();
    }
    public void CookOrder(Order order)
    {
        this.order = order;
        ChefStation availableChefStation = CookManager.Instance.FindAvailableChefstation();
        CookManager.Instance.servedOrders.Add((order, availableChefStation));
        chefStation = availableChefStation;
        SwitchState(new MoveState(this, workstation.CookTransform, new CookOrderState(this, availableChefStation)));
    }

    public void AssignWorkstation(Workstation workstation)
    {
        this.workstation = workstation;
    }
}
