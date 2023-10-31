using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cook State
public class ServeOrderState : State
{
    CookStateMachine stateMachine;

    public ServeOrderState(CookStateMachine stateMachine, ChefStation chefStation)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Serve Order State");
        Debug.Log("COOK: Served order");
        stateMachine.chefStation.ServeOrder(stateMachine.order);
        stateMachine.isIdle = true;
        stateMachine.carriedItem.SetActive(false);
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
