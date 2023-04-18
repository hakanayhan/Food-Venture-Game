using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] public Transform spawnPoint;
    public float maxCustomer;
    [SerializeField] private RandomPosition _randomPos;
    public List<CashierStation> cashierStations = new List<CashierStation>();
    public List<CustomerStateMachine> customers = new List<CustomerStateMachine>();
    public List<OrderItem> orderItems;
    public int nextOrderID = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (customers.Count < maxCustomer)
            SpawnNewCustomer();
    }
    private void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(_customerPrefab, spawnPoint.position + _randomPos.GetRandomPos(), Quaternion.identity);
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
        //Debug.LogError("No free CashierStations");
        return null;
    }

    public void AddItem(OrderItem orderItem)
    {
        bool alreadyExist = orderItems.Contains(orderItem);
        if (!alreadyExist)
            orderItems.Add(orderItem);
    }
}