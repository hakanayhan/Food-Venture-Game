using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text goldText;

    public void SetGoldText(double goldAmount)
    {
        goldText.text = goldAmount.ToString();
    }
}