using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    public GameObject window;
    public Camera cameraGameObject;
    [SerializeField] ScrollRect inventoryScrollRect;
    public void OpenInventory()
    {
        window.SetActive(true);
        cameraGameObject.GetComponent<CameraController>().enabled = false;
        inventoryScrollRect.verticalNormalizedPosition = 1f;
    }

    public void CloseInventory()
    {
        window.SetActive(false);
        cameraGameObject.GetComponent<CameraController>().enabled = true;
    }
}
