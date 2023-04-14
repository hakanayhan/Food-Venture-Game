using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image progressBar;

    public void SetFillAmount(float fillAmount)
    {
        progressBar.fillAmount = fillAmount;
    }

    public void ResetFillAmount()
    {
        SetFillAmount(0f);
    }
}