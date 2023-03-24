using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialTimer : MonoBehaviour
{
    [SerializeField] GameObject outerRing;
    [SerializeField] GameObject fill;
    [SerializeField] GameObject innerRing;

    Image fillImage;

    float timer = 0f;
    float maxTime = 0f;

    public void StartTimer(float timer)
    {
        transform.eulerAngles = new Vector3(90, 0, 0);

        fillImage = fill.GetComponent<Image>();
        fillImage.fillAmount = 0f;

        this.timer = timer;
        this.maxTime = timer;

        outerRing.SetActive(true);
        fill.SetActive(true);
        innerRing.SetActive(true);

    }

    void Update()
    {
        if (timer > 0f)
        {
            timer = Mathf.Max(0f, timer -= Time.deltaTime);
            fillImage.fillAmount = 1f - timer / maxTime;
        }
        else
        {
            FinishTimer();
        }
    }

    void FinishTimer()
    {
        outerRing.SetActive(false);
        fill.SetActive(false);
        innerRing.SetActive(false);
    }
}
