using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cook State
public class CookOrderState : State
{
    CookStateMachine stateMachine;

    float workTimer;

    public CookOrderState(CookStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Cook Order State");
        workTimer = stateMachine.order.orderItem.GetCookTime();
        stateMachine.radialTimer.StartTimer(workTimer);
        stateMachine.workstation.isBusy = true;
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Cook Order State");
        workTimer = Mathf.Max(0f, workTimer -= deltaTime);

        if (workTimer == 0f)
        {
            //Debug.Log("COOK: Serving order to Serverstation");
            stateMachine.workstation.isBusy = false;
            if (!stateMachine.cookManager.hasCooks)
            {
                stateMachine.carriedItem.GetComponent<MeshRenderer>().material = stateMachine.order.orderItem.itemMaterial;
                stateMachine.carriedItem.SetActive(true);
                stateMachine.SwitchState(new MoveState(stateMachine, stateMachine.order.customer.cashierStation.CashierTransform, new FulfillOrderState(stateMachine.cashierStateMachine, stateMachine.order.customer.cashierStation)));
            }
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Cook Order State");
    }
}
