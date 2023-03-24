using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    StateMachine stateMachine;

    public IdleState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.isIdle = true;
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
        stateMachine.isIdle = false;
    }
}
