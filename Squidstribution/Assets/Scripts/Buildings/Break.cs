using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject fractured;
    [SerializeField]
    private float breakForce;
    private Building buildingStats;
    CalculateKarma calc;

    private static int buildingsDestroyed;

    private void Start()
    {
        buildingStats = gameObject.GetComponent<Building>();
        calc = GameObject.Find("KarmaKalculator").GetComponent<CalculateKarma>();
        if (buildingStats.GetDistrict() != null)
        {
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount() + 1);
        }
    }

    public void ApplyDamage(int damage)
    {
        buildingStats.SetHealth(buildingStats.GetHealth() - damage);

        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Play(true);
        }

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
        calc.setBuildingValue(buildingStats.GetKarmaScore());
        Destroy(gameObject);
        buildingsDestroyed++;

        if(buildingsDestroyed == 1)
        {
            EventManager.TriggerEvent("FirstBuildingDestroyed");
        }

        if(buildingsDestroyed == 5)
        {
            EventManager.TriggerEvent("FiveBuildingsDestroyed");
        }
    }
}
