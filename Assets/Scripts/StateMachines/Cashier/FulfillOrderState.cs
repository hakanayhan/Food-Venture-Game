using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cashier State
public class FulfillOrderState : State
{
    CashierStateMachine stateMachine;
    CashierStation cashierStation;

    public FulfillOrderState(CashierStateMachine stateMachine, CashierStation cashierStation)
    {
        this.stateMachine = stateMachine;
        this.cashierStation = cashierStation;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Fulfill Order State");

        cashierStation.FulfillOrder();
        Debug.Log("CASH: Delivered Order to Cashierstation");

        stateMachine.SwitchState(new IdleState(stateMachine));
        cashierStation.customer.Pay();
        cashierStation.customer.CustomerExit();
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Fulfill Order State");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Fulfill Order State");
    }
}
