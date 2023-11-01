using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    public static CashierManager Instance;
    [SerializeField] private GameObject _cashierPrefab;
    [SerializeField] private Transform _spawnPoint;
    public float maxCashier;
    [SerializeField] private RandomPosition _randomPos;
    public List<CashierStateMachine> cashiers = new List<CashierStateMachine>();
    public List<CashierStation> cashierStations = new List<CashierStation>();
    private float _currentCashier = 0;
    
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
        if (_currentCashier < maxCashier)
            SpawnNewCashier();

        ManageOrders();
    }

    void ManageOrders()
    {
        TakeAvailableOrders();
        if(CookManager.Instance.hasCooks)
            DeliverOrders();
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
                List<CashierStateMachine> availableCashiers = new List<CashierStateMachine>();
                foreach (CashierStateMachine cashier in cashiers)
                {
                    if (cashier.isIdle)
                    {
                        availableCashiers.Add(cashier);
                    }
                }
                if(availableCashiers.Count != 0)
                {
                    CashierStateMachine closestCashier = GetClosestCashier(availableCashiers, cashierStation.CashierTransform);
                    closestCashier.TakeOrder(cashierStation);
                }
            }
        }
    }

    void DeliverOrders()
    {
        foreach ((Order order, ChefStation chefStation) in CookManager.Instance.servedOrders)
        {
            if (!chefStation.isReservedByCashier && chefStation.hasOrder)
            {
                List<CashierStateMachine> availableCashiers = new List<CashierStateMachine>();
                foreach (CashierStateMachine cashier in cashiers)
                {
                    if (cashier.isIdle)
                    {
                        availableCashiers.Add(cashier);
                    }
                }
                if (availableCashiers.Count != 0)
                {
                    CashierStateMachine closestCashier = GetClosestCashier(availableCashiers, chefStation.CashierTransform);
                    closestCashier.DeliverOrder(order, chefStation);
                }
            }
        }
    }
    public CashierStateMachine GetClosestCashier(List<CashierStateMachine> cashiers, Transform targetObject)
    {
        CashierStateMachine closestCashier = null;
        float minDist = Mathf.Infinity;
        foreach (CashierStateMachine cashier in cashiers)
        {
            Transform t = cashier.gameObject.transform;
            float dist = Vector3.Distance(t.position, targetObject.position);
            if (dist < minDist)
            {
                closestCashier = cashier;
                minDist = dist;
            }
        }
        return closestCashier;
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
