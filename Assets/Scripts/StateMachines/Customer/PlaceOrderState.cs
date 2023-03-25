using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOrderState : State
{
    CustomerStateMachine stateMachine;

    public PlaceOrderState(CustomerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.cashierStation.PlaceOrder(stateMachine.order);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
