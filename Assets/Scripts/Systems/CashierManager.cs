using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    [SerializeField] private GameObject _cashierPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _maxCashier;
    [SerializeField] private RandomPosition _randomPos;
    public List<CashierStateMachine> cashiers = new List<CashierStateMachine>();
    public List<CashierStation> cashierStations = new List<CashierStation>();
    private float _currentCashier = 0;

    private void Update()
    {
        if (_currentCashier < _maxCashier)
            SpawnNewCashier();

        ManageOrders();
    }

    void ManageOrders()
    {
        TakeAvailableOrders();
    }

    private void SpawnNewCashier()
    {
        GameObject cashierGameObject = Instantiate(_cashierPrefab, _spawnPoint.position + _randomPos.GetRandomPos(), Quaternion.identity);
        CashierStateMachine cashier = cashierGameObject.GetComponent<CashierStateMachine>();
        _currentCashier++;
        cashiers.Add(cashier);
    }

    void TakeAvailableOrders()
    {
        foreach (CashierStation cashierStation in cashierStations)
        {
            if (cashierStation.hasCustomer && !cashierStation.isReservedByCashier)
            {
                foreach (CashierStateMachine cashier in cashiers)
                {
                    if (cashier.isIdle)
                    {
                        cashier.TakeOrder(cashierStation);
                        return;
                    }
                }
            }
        }
    }

    public bool areOrdersTaken()
    {
        CashierStation orders = FindAvailableOrders();
        if (orders != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public CashierStation FindAvailableOrders()
    {
        foreach (CashierStation cashierStation in cashierStations)
        {
            if (cashierStation.hasCustomer && !cashierStation.isReservedByCashier)
            {
                return cashierStation;
            }
        }
        return null;
    }
}
