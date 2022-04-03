using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] int maximum;
    [SerializeField] int current;
    [SerializeField] Image mask;
    [SerializeField] Text currentPercent;

    public void SetMaximum(int newMax)
    {
        maximum = newMax;
    }

    public void SetCurrent(int districtCurrent)
    {
        current = districtCurrent;
    }

    public void SetText(Text percent)
    {
        currentPercent = percent;
    }

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;

        mask.fillAmount = 1 - fillAmount;
        currentPercent.text = (int)(mask.fillAmount * 100) + "%";
    }
}
