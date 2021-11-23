using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject destroyedPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            DestroyBuilding();
        }
    }

    void DestroyBuilding()
    {
        Instantiate(destroyedPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
