using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredObject : MonoBehaviour
{
    private enum PowerTypes {HEALER, BATTERY }

    [SerializeField] PowerTypes powerMethod;
    [SerializeField] List<GameObject> powerSources;

    void Update()
    {
        if(powerMethod == PowerTypes.BATTERY)
        {
            HandleBatterySource();
        }
        
    }

    void HandleBatterySource()
    {
        foreach (GameObject Source in powerSources)
        {
            if (Source.activeInHierarchy == false)
            {
                gameObject.GetComponent<MeshRenderer>().material.SetFloat("Power", 2f);
                powerSources.Remove(Source);
            }
        }

        if (powerSources.Count == 0)
        {

            gameObject.SetActive(false);
        }
    }
}
