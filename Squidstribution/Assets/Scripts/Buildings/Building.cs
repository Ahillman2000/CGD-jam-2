using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] District district;

    [SerializeField] float health = 100;
    private float startHealth;
    [SerializeField] GameObject destroyedPrefab;

    private void Awake()
    {
        if(district != null)
        {
            district.SetBuildingCount(district.GetBuildingCount() + 1);
        }
    }

    private void Start()
    {
        startHealth = health;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            DestroyBuilding();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetStartHealth()
    {
        return startHealth;
    }

    void DestroyBuilding()
    {
        district.SetDestruction(district.GetDestruction() + district.GetDestructionPointsPerBuilding());

        Instantiate(destroyedPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
