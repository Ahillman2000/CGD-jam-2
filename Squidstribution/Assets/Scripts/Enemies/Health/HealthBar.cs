using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private void OnEnable()
    {
        if(GetComponentInParent<Health>() != null)
        {
            GetComponentInParent<Health>().onHealthChange += UpdateHealthBar;
        } else { Debug.Log("Parent object " + this.transform.parent + " of " + this.gameObject + " does not contain a Health script!"); }
    }

    private void OnDisable()
    {
        //[Issue]: Will throw a null reference if the attached gameobject is not destroyed
        if (GetComponentInParent<Health>() != null)
        {
            GetComponentInParent<Health>().onHealthChange -= UpdateHealthBar;
        } else { Debug.Log("Parent object " + this.transform.parent + " of " + this.gameObject + " does not contain a Health script, or the object was not destroyed!"); }
    }

    private void Start()
    {
        if (GetComponentInChildren<Slider>() != null)
        {
            GetComponentInChildren<Slider>();
        } else { Debug.Log("Child object " + this.transform.parent + " of " + this.gameObject + " does not contain a Slider!"); }
    }
    
    private void UpdateHealthBar(float damagetaken)
    {
        if (GetComponentInChildren<Slider>() != null)
        {
            GetComponentInChildren<Slider>().value = damagetaken;
        } else { Debug.Log("Child object " + this.transform.parent + " of " + this.gameObject + " does not contain a Slider!"); }
    }
}
