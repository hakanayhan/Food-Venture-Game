using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesWindow : MonoBehaviour
{
    public GameObject window;
    public void OpenUpgrades()
    {
        window.SetActive(true);
    }

    public void CloseUpgrades()
    {
        window.SetActive(false);
    }
}
