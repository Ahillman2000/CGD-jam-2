using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //GameManagerScript gameManagerScript;
    [SerializeField] District district;

    [SerializeField] float health = 100;
    private float startHealth;
    [SerializeField] GameObject destroyedPrefab;

    private void Start()
    {
        //gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        //district = this.GetComponentInParent<District>();

        if (district == null)
        {
            Debug.LogWarning(this.gameObject.name + " requires a district");
        }
        else
        {
            district.SetBuildingCount(district.GetBuildingCount() + 1);
        }

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
        district.SetDestruction(district.GetDestruction() + district.GetDestructionpointsPerBuilding());
        Debug.Log(district.name + "destruction at " + district.GetDestruction() + "%");

        Instantiate(destroyedPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
