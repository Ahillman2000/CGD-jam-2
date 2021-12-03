using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField]
    private GameObject fractured;
    [SerializeField]
    private float breakForce;
    private Building buildingStats;


    private void Awake()
    {
        buildingStats = gameObject.GetComponent<Building>();
    }
    private void Start()
    {
        if (buildingStats.GetDistrict() != null)
        {
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount() + 1);
        }
    }

    public void TakeDamage(float _damage)
    {
        buildingStats.SetHealth(buildingStats.GetHealth() - _damage);
        if (buildingStats.GetHealth() <= 0)
        {
           BreakThing();
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
        buildingStats.GetDistrict().SetDestruction(buildingStats.GetDistrict().GetDestruction() + buildingStats.GetDistrict().GetDestructionPointsPerBuilding());
        CalculateKarma calc = GameObject.Find("KarmaKalculator").GetComponent<CalculateKarma>();
        calc.setBuildingValue(buildingStats.GetKarmaScore());
        Destroy(gameObject);
    }
}
