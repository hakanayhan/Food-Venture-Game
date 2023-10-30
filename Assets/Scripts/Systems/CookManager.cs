using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;
    public bool hasCooks;
    public List<Order> pendingOrders = new List<Order>();
    List<CookStateMachine> cooks = new List<CookStateMachine>();
    public List<Workstation> workstations;
    public List<ChefStation> chefstations;
    public CashierManager cashierManager;

    [SerializeField] private GameObject _cookPrefab;
    [SerializeField] private Transform _spawnPoint;
    public float maxCook;
    private float _currentCook = 0;
    [SerializeField] private RandomPosition _randomPos;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cashierManager = FindObjectOfType<CashierManager>();
    }

    private void Update()
    {
        if(hasCooks)
            SpawnCook();

        ManageOrders();
    }
    public void ReceiveOrder(Order order)
    {
        pendingOrders.Add(order);
    }
    
    public void ManageOrders()
    {
        if (pendingOrders.Count > 0)
        {
            if (!hasCooks) // if cashiers are used instead of cooks
            {
                List<CashierStateMachine> availableCashiers = new List<CashierStateMachine>();
                foreach (CashierStateMachine cashier in cashierManager.cashiers)
                {
                    if (cashier.isIdle)
                        availableCashiers.Add(cashier);
                }
                if (availableCashiers.Count != 0 && cashierManager.areOrdersTaken())
                {
                    foreach (Order pendingOrder in pendingOrders)
                    {
                        Workstation availableWorkstation = FindAvailableWorkstation(pendingOrder.orderItem);
                        if (availableWorkstation == null)
                            continue;

                        CashierStateMachine closestCashier = CashierManager.Instance.GetClosestCashier(availableCashiers, availableWorkstation.CookTransform);
                        closestCashier.isIdle = false;
                        closestCashier.cook.AssignWorkstation(availableWorkstation);
                        closestCashier.cook.CookOrder(pendingOrder);
                        pendingOrders.Remove(pendingOrder);
                        return;
                    }
                }
            }
            else
            {
                List<CookStateMachine> availableCooks = new List<CookStateMachine>();
                foreach (CookStateMachine cook in cooks)
                {
                    if (cook.isIdle)
                        availableCooks.Add(cook);
                }
                if(availableCooks.Count != 0)
                {
                    foreach (Order pendingOrder in pendingOrders)
                    {
                        Workstation availableWorkstation = FindAvailableWorkstation(pendingOrder.orderItem);
                        if (availableWorkstation == null)
                            continue;

                        CookStateMachine closestCook = GetClosestCook(availableCooks, availableWorkstation.CookTransform);
                        closestCook.isIdle = false;
                        closestCook.AssignWorkstation(availableWorkstation);
                        closestCook.CookOrder(pendingOrder);
                        pendingOrders.Remove(pendingOrder);
                        return;
                    }
                }
            }
        }
    }

    public CookStateMachine GetClosestCook(List<CookStateMachine> cooks, Transform targetObject)
    {
        CookStateMachine closestCook = null;
        float minDist = Mathf.Infinity;
        foreach (CookStateMachine cook in cooks)
        {
            Transform t = cook.gameObject.transform;
            float dist = Vector3.Distance(t.position, targetObject.position);
            if (dist < minDist)
            {
                closestCook = cook;
                minDist = dist;
            }
        }
        return closestCook;
    }

    public Workstation FindAvailableWorkstation(OrderItem orderItem)
    {
        //Debug.Log("Find Available Workstation out of " + workstations.Count);
        foreach (Workstation workstation in workstations)
        {
            if (!workstation.gameObject.activeInHierarchy)
            {
                 //Debug.Log(workstation.name + " is not active");
                continue;
            }
            if (!workstation.isBusy && workstation.orderItem == orderItem)
            {
                workstation.isBusy = true;
                return workstation;
            }
        }

        //Debug.LogError("Workstation could not be found");
        return null;
    }

    public ChefStation FindAvailableChefstation()
    {
        foreach (ChefStation chefstation in chefstations)
        {
            if (!chefstation.isReservedByCook)
                return chefstation;
        }

        return null;
    }

    private void SpawnCook()
    {
        if (_currentCook < maxCook)
        {
            GameObject cookGameObject = Instantiate(_cookPrefab, _spawnPoint.position + _randomPos.GetRandomPos(), Quaternion.identity);
            CookStateMachine cook = cookGameObject.GetComponent<CookStateMachine>();
            cook.isIdle = true;
            _currentCook++;
            cooks.Add(cook);
        }
    }
}
