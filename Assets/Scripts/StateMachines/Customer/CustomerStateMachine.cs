using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerStateMachine : MonoBehaviour
{
    public CashierStation cashierStation;
    public NavMeshAgent navMeshAgent;
    public Order order;

    public void AssignCashierstation(CashierStation cashierStation, CustomerStateMachine customer)
    {
        this.cashierStation = cashierStation;
        cashierStation.ReserveStation(this);
        customer.navMeshAgent.SetDestination(cashierStation.CustomerTransform.position);
    }
    public void AssignOrder(Order order)
    {
        this.order = order;
    }
}
