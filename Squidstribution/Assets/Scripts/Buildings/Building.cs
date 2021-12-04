using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] District district;

    /*[SerializeField]*/ private float health;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float KarmaScore = 25;

    private void Awake()
    {
        if(district != null)
        {
            district.SetBuildingCount(district.GetBuildingCount());
        }
    }

    private void Start()
    {
        health = startHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float health_)
    {
        health = health_;
    }

    public District GetDistrict()
    {
        return district;
    }

    public float GetKarmaScore()
    {
        return KarmaScore;
    }
}
