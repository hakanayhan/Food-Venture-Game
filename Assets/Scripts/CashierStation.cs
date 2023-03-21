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
    

    public void ReserveStation(CustomerStateMachine customer)
    {
        isReservedByCustomer = true;
        hasCustomer = true;
    }
    public void ReserveStation(CashierStateMachine cashier)
    {
        isReservedByCashier = true;
    }
}
