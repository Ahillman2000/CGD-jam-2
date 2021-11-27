using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    private float health = 100;
    private GameObject currentDistrict;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("District"))
        {
            Squid squid = this.GetComponent<Squid>();

            squid.SetCurrentDistrict(other.gameObject);
            Debug.Log("Player has entered " + GetCurrentDistrict());
            Debug.Log("Destruction for this district is " + GetCurrentDistrictDestruction() + "%");
        }
    }

    public void setHealth(float _health)
    {
        health = _health;
    }
    public float getHealth()
    {
        return health;
    }

    public void SetCurrentDistrict(GameObject _district)
    {
        currentDistrict = _district;
    }
    public GameObject GetCurrentDistrict()
    {
        return currentDistrict;
    }

    public float GetCurrentDistrictDestruction()
    {
        District district = GetCurrentDistrict().GetComponent<District>();
        return district.GetDestruction();
    }

    void Update()
    {
        
    }
}
