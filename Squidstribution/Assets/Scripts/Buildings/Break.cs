using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;

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

    private void Update()
    {
        /*if (buildingStats.GetDistrict() != null)
        {
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount());
        }*/
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
            buildingStats.GetDistrict().SetBuildingCount(buildingStats.GetDistrict().GetBuildingCount() - 1);
        }
    }

    private void BreakThing()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject frac = Instantiate(fractured, gameObject.transform.position, transform.rotation);

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
        Debug.Log("asjdil");
        //would this not be better server on an event manager class?
        /// I don't think so. This reduces the coupling between classes.
        /// If this were in an event manager class the event manager
        /// would then need to know about 'Building' (among other classes/
        /// components if the same were done for other events). So instead
        /// we can invoke an event here and other classes can listen and
        /// subscribe to the event if they want to. - Charlie.
    }
}
