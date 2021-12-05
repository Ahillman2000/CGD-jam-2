using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] District district;

    /*[SerializeField]*/ private float health;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float KarmaScore = 25;

    //[HideInInspector] public Material defaultMat;
    [HideInInspector] public Material[] defaultMat;
    public Material highlightMat;

    private void Awake()
    {
        defaultMat = GetComponent<MeshRenderer>().materials;

        if(district != null)
        {
            district.SetBuildingCount(district.GetBuildingCount());
        }
    }

    private void Start()
    {
        health = startHealth;
    }

    public float GetStartHealth()
    {
        return startHealth;
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
