using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    void OnMouseDown()
    {
        transform.parent.parent.GetComponent<WorkStationUpgrader>().OnChildClicked();
    }
}
