using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CashierStateMachine : StateMachine
{
    public CashierStation cashierStation;
    public Order order;
    public NavMeshAgent navMeshAgent;

    public void TakeOrder(CashierStation cashierStation, CashierStateMachine cashier)
    {
        this.cashierStation = cashierStation;
        cashierStation.ReserveStation(this);
        SwitchState(new MoveState(this, cashierStation.CashierTransform, new TakeOrderState(this)));
        isIdle = false;
        Debug.Log("take order");
    }

}
