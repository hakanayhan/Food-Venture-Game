using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeServedItemState : State
{
    CashierStateMachine stateMachine;
    ChefStation chefStation;
    Order order;
    public TakeServedItemState(CashierStateMachine stateMachine, ChefStation chefStation, Order order)
    {
        this.stateMachine = stateMachine;
        this.chefStation = chefStation;
        this.order = order;
    }
    public override void Enter()
    {

        stateMachine.cook.carriedItem.GetComponent<MeshRenderer>().material = order.orderItem.itemMaterial;
        stateMachine.cook.carriedItem.SetActive(true);
        chefStation.servedItem.SetActive(false);
        chefStation.hasOrder = false;
        chefStation.isReservedByCook = false;
        chefStation.isReservedByCashier = false;
        stateMachine.SwitchState(new MoveState(stateMachine, order.customer.cashierStation.CashierTransform, new FulfillOrderState(stateMachine, order.customer.cashierStation)));
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {

    }
}
