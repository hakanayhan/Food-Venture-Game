using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public bool isItUnlocker;
    void OnMouseUp()
    {
        transform.parent.parent.GetComponent<WorkstationUpgrader>().OnChildClicked(isItUnlocker);
    }
}
