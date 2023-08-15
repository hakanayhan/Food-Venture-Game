using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsController : MonoBehaviour
{
    public static EquippedItemsController Instance;
    public EquipmentController equippedHatController;
    public EquipmentController equippedApronController;
    public EquipmentController equippedSpatulaController;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void RefreshEquipments()
    {
        equippedHatController.Refresh();
        equippedApronController.Refresh();
        equippedSpatulaController.Refresh();
    }
}
