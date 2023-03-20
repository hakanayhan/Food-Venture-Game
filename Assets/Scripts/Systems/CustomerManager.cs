using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject _customerPrefab;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _maxCustomer;
    private float _currentCustomer = 0;

    private void Update()
    {
        if (_currentCustomer < _maxCustomer)
        {
            SpawnNewCustomer();
        }
    }

    private void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(_customerPrefab, _spawnPoint.position, Quaternion.identity);
        _currentCustomer++;
    }
}
