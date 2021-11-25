using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    GameManagerScript gameManagerScript;

    [SerializeField] float health = 100;
    private float startHealth;
    [SerializeField] GameObject destroyedPrefab;

    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        gameManagerScript.SetBuildingCount(gameManagerScript.GetBuildingCount() + 1);
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
        gameManagerScript.SetDestruction(gameManagerScript.GetDestruction() + gameManagerScript.GetDestructionpointsPerBuilding());

        Instantiate(destroyedPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
