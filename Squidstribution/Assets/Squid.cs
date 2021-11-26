using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    float health = 100;
    GameObject currentDistrict;

    void Start()
    {
        
    }

    public void SetCurrentDistrict(GameObject _district)
    {
        currentDistrict = _district;
    }
    public GameObject GetCurrentDistrict()
    {
        return currentDistrict;
    }

    void Update()
    {
        
    }
}
