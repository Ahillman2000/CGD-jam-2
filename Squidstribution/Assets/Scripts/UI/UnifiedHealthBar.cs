using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnifiedHealthBar : MonoBehaviour
{
    [SerializeField] List<Building> HealthSources;
    float CurrentFill;
    float FilledAmount;
    Slider slider;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        foreach(Building Source in HealthSources)
        {
            FilledAmount += Source.GetStartHealth();
        }
        CurrentFill = FilledAmount;
        slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float CombinedHealth = CurrentFill / FilledAmount;
        slider.value = CombinedHealth;
    }

    public void SetUnifiedHealth()
    {
        CurrentFill = 0f;
        foreach(Building Source in HealthSources)
        {
            CurrentFill += (float)Source.GetHealth();
        }

        if(CurrentFill == 0)
        {
            slider.gameObject.SetActive(false);
        }
    }
}
