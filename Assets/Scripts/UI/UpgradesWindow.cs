using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesWindow : MonoBehaviour
{
    public GameObject window;
    public Camera cameraGameObject;
    [SerializeField] ScrollRect upgradesScrollRect;

    public void OpenUpgrades()
    {
        window.SetActive(true);
        cameraGameObject.GetComponent<CameraController>().enabled = false;
        upgradesScrollRect.verticalNormalizedPosition = 1f;
    }

    public void CloseUpgrades()
    {
        window.SetActive(false);
        cameraGameObject.GetComponent<CameraController>().enabled = true;
    }
}
