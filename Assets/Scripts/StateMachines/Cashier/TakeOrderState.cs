using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrderState : State
{
    CashierStateMachine stateMachine;
    float takeOrderTimer;
    public TakeOrderState(CashierStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public override void Enter()
    {
        takeOrderTimer = stateMachine.cashierTakeOrderTimer;
        stateMachine.radialTimer.StartTimer(takeOrderTimer);
    }

    public override void Tick(float deltaTime)
    {
        takeOrderTimer = Mathf.Max(0f, takeOrderTimer -= deltaTime);

        if (takeOrderTimer == 0f)
        {
            Debug.Log("Order Taken");
            Order order = stateMachine.cashierStation.TakeOrder();
            stateMachine.SwitchState(new IdleState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
