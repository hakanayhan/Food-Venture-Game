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
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
