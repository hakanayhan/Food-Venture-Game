using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrderState : State
{
    CashierStateMachine stateMachine;
    public TakeOrderState(CashierStateMachine stateMachine)
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
