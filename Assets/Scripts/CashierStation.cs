using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierStation : MonoBehaviour
{
    public bool isReservedByCustomer;
    public bool isReservedByCashier;
    public Transform CustomerTransform;
    public Transform CashierTransform;
    public bool hasCustomer;
    public bool hasOrder;
    public Order order;



    public void ReserveStation(CustomerStateMachine customer)
    {
        isReservedByCustomer = true;
        hasCustomer = true;
    }
    public void ReserveStation(CashierStateMachine cashier)
    {
        isReservedByCashier = true;
    }

    public Order TakeOrder()
    {
        hasOrder = true;
        order.customer.ShowSpeechBubble();
        return order;
    }
}
