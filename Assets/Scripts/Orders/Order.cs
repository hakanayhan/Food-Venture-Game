using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Order
{
    public int orderID;
    public OrderItem orderItem;
    public int quantity;
    public CustomerStateMachine customer;

    public Order(int orderID, OrderItem orderItem, int quantity, CustomerStateMachine customer)
    {
        this.orderID = orderID;
        this.orderItem = orderItem;
        this.quantity = quantity;
        this.customer = customer;
    }
}
