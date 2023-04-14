using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerStateMachine : StateMachine
{
    public CashierStation cashierStation;
    public Order order;
    public GameObject speechBubble;
    public CustomerManager customerManager;
    public CookManager cookManager;
    public Wallet wallet;

    private void Start()
    {
        customerManager = FindObjectOfType<CustomerManager>();
        cookManager = FindObjectOfType<CookManager>();
        wallet = FindObjectOfType<Wallet>();
    }
    public void AssignCashierstation(CashierStation cashierStation, CustomerStateMachine customer)
    {
        this.cashierStation = cashierStation;
        cashierStation.ReserveStation(this);
        SwitchState(new MoveState(this, cashierStation.CustomerTransform, new PlaceOrderState(this)));
    }
    public void ShowSpeechBubble()
    {
        speechBubble.transform.Find("Icon").GetComponent<Image>().sprite = order.orderItem.itemIcon;
        speechBubble.transform.eulerAngles = new Vector3(90, 0, 0);
        speechBubble.SetActive(true);
    }
    public void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }

    public void Order() => AssignOrder(GenerateOrder(this));
    public void AssignOrder(Order order)
    {
        this.order = order;
    }

    public void Pay()
    {
        wallet.AddGold((double)order.orderItem.GetCost());
    }

    public void CustomerExit()
    {
        HideSpeechBubble();
        SwitchState(new MoveState(this, customerManager.spawnPoint, new CustomerExitState(this)));
    }
    public void Despawn()
    {
        cashierStation.hasCustomer = false;
        cashierStation.hasOrder = false;
        cashierStation.isReservedByCustomer = false;
        cashierStation.isReservedByCashier = false;
        Destroy(gameObject);
        customerManager.customers.Remove(this);
    }

    Order GenerateOrder(CustomerStateMachine customer)
    {
        int itemToOrderIndex = Random.Range(0, customerManager.orderItems.Count);
        OrderItem itemToOrder = customerManager.orderItems[itemToOrderIndex];
        Order order = new Order(customerManager.nextOrderID, itemToOrder, 1, customer);
        customerManager.nextOrderID++;
        //Debug.Log(customer + " ordered " + itemToOrder.itemName);
        cookManager.pendingOrders.Add(order);
        return order;
    }
}
