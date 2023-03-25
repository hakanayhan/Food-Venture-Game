using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cook State
public class ServeOrderState : State
{
    CashierStateMachine stateMachine;

    public ServeOrderState(CashierStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Serve Order State");
        Debug.Log("COOK: Served order");
        stateMachine.cashierStation.ServeOrder(stateMachine.order);
        stateMachine.cashierStation.customer.CustomerExit();
        stateMachine.isIdle = true;
        //stateMachine.SwitchState(new IdleState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Serve Order State");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Serve Order State");
    }
}
