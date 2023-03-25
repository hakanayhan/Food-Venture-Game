using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerExitState : State
{
    CustomerStateMachine stateMachine;

    public CustomerExitState(CustomerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.Despawn();
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
