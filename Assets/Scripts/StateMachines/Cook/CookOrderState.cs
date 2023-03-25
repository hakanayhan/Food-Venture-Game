using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cook State
public class CookOrderState : State
{
    CashierStateMachine stateMachine;

    float workTimer;

    public CookOrderState(CashierStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Entering Cook Order State");
        workTimer = stateMachine.order.orderItem.GetCookTime();
        stateMachine.radialTimer.StartTimer(workTimer);
        stateMachine.workStation.isBusy = true;
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("Ticking Cook Order State");
        workTimer = Mathf.Max(0f, workTimer -= deltaTime);

        if (workTimer == 0f)
        {
            Debug.Log("COOK: Serving order to Serverstation");
            stateMachine.workStation.isBusy = false;
            stateMachine.SwitchState(new MoveState(stateMachine, stateMachine.cashierStation.CashierTransform, new ServeOrderState(stateMachine)));
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Cook Order State");
    }
}
