using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CashierStateMachine : StateMachine
{
    public CashierStation cashierStation;
    public Order order;
    public NavMeshAgent navMeshAgent;
    public RadialTimer radialTimer;

    [HideInInspector] public WorkStation workStation;
    public void TakeOrder(CashierStation cashierStation)
    {
        this.cashierStation = cashierStation;
        cashierStation.ReserveStation(this);
        SwitchState(new MoveState(this, cashierStation.CashierTransform, new TakeOrderState(this)));
        isIdle = false;
        Debug.Log("take order");
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
