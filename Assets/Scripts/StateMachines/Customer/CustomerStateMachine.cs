using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerStateMachine : StateMachine
{
    public CashierStation cashierStation;
    public Order order;

    public void AssignCashierstation(CashierStation cashierStation, CustomerStateMachine customer)
    {
        this.cashierStation = cashierStation;
        cashierStation.ReserveStation(this);
        SwitchState(new MoveState(this, cashierStation.CustomerTransform, new PlaceOrderState(this)));
    }
    public void AssignOrder(Order order)
    {
        this.order = order;
    }
}
