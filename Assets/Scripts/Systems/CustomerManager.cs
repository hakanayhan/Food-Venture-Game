using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _maxCustomer;
    public List<CashierStation> cashierStations = new List<CashierStation>();
    private float _currentCustomer = 0;
    public List<CustomerStateMachine> customers = new List<CustomerStateMachine>();
    public List<OrderItem> orderItems;
    public int nextOrderID = 0;

    private void Update()
    {
        if (_currentCustomer < _maxCustomer)
            SpawnNewCustomer();
    }

    private void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(_customerPrefab, _spawnPoint.position, Quaternion.identity);
        CustomerStateMachine customer = customerGameObject.GetComponent<CustomerStateMachine>();
        CashierStation cashierstation = FindFreeCashierstation();
        customer.AssignCashierstation(cashierstation, customer);
        cashierstation.customer = customer;
        //customer.AssignOrder(GenerateOrder(customer));
        customers.Add(customer);
        _currentCustomer++;
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