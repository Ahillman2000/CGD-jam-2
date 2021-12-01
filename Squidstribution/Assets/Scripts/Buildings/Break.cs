using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField] District district;

    [SerializeField]
    private GameObject fractured;
    [SerializeField]
    private float breakForce;
    [SerializeField]
    private float start_health = 100f;
    private float health;

    public bool inRange = false;

    private void Awake()
    {
        if (district != null)
        {
            district.SetBuildingCount(district.GetBuildingCount() + 1);
        }
    }

    private void Start()
    {
        health = start_health;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
           BreakThing();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("player in range of building");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("player out of range of building");
        }
    }

    private void BreakThing()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject frac = Instantiate(fractured, gameObject.transform.position, Quaternion.identity);

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddExplosionForce(10, frac.transform.position,3);
        }
        this.gameObject.SetActive(false);
        district.SetDestruction(district.GetDestruction() + district.GetDestructionPointsPerBuilding());
        Destroy(gameObject);
    }
}
