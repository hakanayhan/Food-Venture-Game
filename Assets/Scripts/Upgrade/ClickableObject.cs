using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public bool isItUnlocker;
    void OnMouseDown()
    {
        transform.parent.parent.GetComponent<WorkstationUpgrader>().OnChildClicked(isItUnlocker);
    }
}
