using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredObject : MonoBehaviour
{
    private enum PowerTypes {HEALER, BATTERY }

    [SerializeField] PowerTypes powerMethod;
    [SerializeField] List<GameObject> powerSources;

    [SerializeField] ChefBoss HealTarget;

    private void Start()
    {
        if(HealTarget == null)
        {
            HealTarget = FindObjectOfType<ChefBoss>();
        }
    }

    void Update()
    {
        if(powerMethod == PowerTypes.BATTERY)
        {
            HandleBatterySource();
        }
        
    }

    private void OnDisable()
    {
        if (HealTarget != null)
        {
            foreach (PoweredObject source in HealTarget.HealSources)
            {
                if (source == this)
                {
                    HealTarget.HealSources.Remove(source);
                }
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if(powerMethod == PowerTypes.HEALER)
        {
            if(other.gameObject == HealTarget.gameObject)
            {
                InvokeRepeating("HealBoss", 1f, 0.75f);
            }

            if(other.GetComponent<Break>())
            {
                other.GetComponent<Break>().ApplyDamage(10000);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (powerMethod == PowerTypes.HEALER)
        {
            if (other.gameObject == HealTarget.gameObject)
            {
                CancelInvoke();
            }
        }
    }

    private void HealBoss()
    {
        HealTarget.ApplyDamage(-25);
    }

}
