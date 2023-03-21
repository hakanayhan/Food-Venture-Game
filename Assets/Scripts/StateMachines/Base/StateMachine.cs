using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateMachine : MonoBehaviour
{
    State currentState;

    public NavMeshAgent agent { get; private set; }

    [HideInInspector]
    public bool isIdle;

    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
}
