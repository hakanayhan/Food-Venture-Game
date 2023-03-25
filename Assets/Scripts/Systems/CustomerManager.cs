using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] private float _maxCustomer;
    public List<CashierStation> cashierStations = new List<CashierStation>();
    public List<CustomerStateMachine> customers = new List<CustomerStateMachine>();
    public List<OrderItem> orderItems;
    public int nextOrderID = 0;

    private void Update()
    {
        if (customers.Count < _maxCustomer)
            SpawnNewCustomer();
    }

    private void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(_customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerStateMachine customer = customerGameObject.GetComponent<CustomerStateMachine>();
        CashierStation cashierstation = FindFreeCashierstation();
        customer.AssignCashierstation(cashierstation, customer);
        cashierstation.customer = customer;
        customers.Add(customer);
    }

    CashierStation FindFreeCashierstation()
    {
        foreach (CashierStation cashierstation in cashierStations)
        {
            if (!cashierstation.isReservedByCustomer)
            {
                return cashierstation;
            }
        }
        Debug.LogError("No free CashierStations");
        return null;
    }
}