using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierStation : MonoBehaviour
{
    public bool isReservedByCustomer;
    public Transform CustomerTransform;

    public void ReserveStation(CustomerStateMachine customer)
    {
        isReservedByCustomer = true;
    }
}
