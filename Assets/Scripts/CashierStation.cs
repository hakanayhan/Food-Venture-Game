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
    public CustomerStateMachine customer;


    public void PlaceOrder(Order order)
    {
        hasCustomer = true;
    }
    public void ReserveStation(CustomerStateMachine customer)
    {
        isReservedByCustomer = true;
    }
    public void ReserveStation(CashierStateMachine cashier)
    {
        isReservedByCashier = true;
    }

    public Order TakeOrder()
    {
        hasOrder = true;
        customer.Order();
        customer.ShowSpeechBubble();
        //Debug.Log(customer.order.orderItem);
        return customer.order;
    }

    public void FulfillOrder()
    {

    }

    public void ServeOrder(Order order)
    {
    }
}
