using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Order Item", order = 1)]
public class OrderItem : ScriptableObject
{
    public string itemName;
    public float itemBaseCookTime;
    public float GetCookTime()
    {
        return itemBaseCookTime;
    }
}
