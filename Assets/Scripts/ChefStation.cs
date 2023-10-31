using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefStation : MonoBehaviour
{
    public Order order;
    [SerializeField] private GameObject servedItem;
    public bool isReservedByCashier;
    public bool isReservedByCook;
    public Transform CashierTransform;
    public Transform CookTransform;
    public bool hasCustomer;
    public bool hasOrder;
    public CookStateMachine cook;

    public void ReserveStation(CookStateMachine cook)
    {
        isReservedByCook = true;
    }
    public void ReserveStation(CashierStateMachine cashier)
    {
        isReservedByCashier = true;
    }

    public void ServeOrder(Order order)
    {
        this.order = order;
        hasOrder = true;
        servedItem.GetComponent<MeshRenderer>().material = order.orderItem.itemMaterial;
        servedItem.SetActive(true);
    }
}
