using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookStateMachine : StateMachine
{
    public Order order;
    [HideInInspector] public WorkStation workStation;
    [HideInInspector] public CookManager cookManager;
    public RadialTimer radialTimer;
    public CashierStateMachine cashierStateMachine;

    private void Start()
    {
        cookManager = FindObjectOfType<CookManager>();
    }
    public void CookOrder(Order order)
    {
        this.order = order;
        SwitchState(new MoveState(this, workStation.CookTransform, new CookOrderState(this)));
    }

    public void AssignWorkstation(WorkStation workStation)
    {
        this.workStation = workStation;
    }
}
