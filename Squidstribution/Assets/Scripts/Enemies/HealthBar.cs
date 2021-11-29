using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private void OnEnable()
    {
        // Refactor to <Health> 
        GetComponentInParent<Health>().onHealthChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        //[Issue]: Will throw a null reference if the attached gameobject is not destroyed
        GetComponentInParent<Health>().onHealthChange -= UpdateHealthBar;
    }

    private void Start()
    {
        GetComponentInChildren<Slider>();
    }
    
    private void UpdateHealthBar(float damagetaken)
    {
        GetComponentInChildren<Slider>().value = damagetaken;
    }
}
